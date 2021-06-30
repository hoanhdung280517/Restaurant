using RSSolution.Application.Common;
using RSSolution.Data.EF;
using RSSolution.Data.Entities;
using RSSolution.Utilities.Constants;
using RSSolution.Utilities.Exceptions;
using RSSolution.ViewModels.Catalog.MealImages;
using RSSolution.ViewModels.Catalog.Meals;
using RSSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.Application.Catalog.Meals
{
    public class MealService : IMealService
    {
        private readonly RSDbContext _context;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public MealService(RSDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<int> AddImage(int mealId, MealImageCreateRequest request)
        {
            var mealImage = new MealImage()
            {
                Caption = request.Caption,
                DateCreated = DateTime.Now,
                IsDefault = request.IsDefault,
                MealId = mealId,
                SortOrder = request.SortOrder
            };

            if (request.ImageFile != null)
            {
                mealImage.ImagePath = await this.SaveFile(request.ImageFile);
                mealImage.FileSize = request.ImageFile.Length;
            }
            _context.MealImages.Add(mealImage);
            await _context.SaveChangesAsync();
            return mealImage.Id;
        }

        public async Task AddViewcount(int mealId)
        {
            var meal = await _context.Meals.FindAsync(mealId);
            meal.ViewCount += 1;
            await _context.SaveChangesAsync();
        }

        public async Task<int> Create(MealCreateRequest request)
        {
            var meal = new Meal()
            {
                Price = request.Price,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                MealTranslations = new List<MealTranslation>()
                {
                    new MealTranslation()
                    {
                        Name =  request.Name,
                        Description = request.Description,
                        Details = request.Details,
                        SeoDescription = request.SeoDescription,
                        SeoAlias = request.SeoAlias,
                        SeoTitle = request.SeoTitle,
                        LanguageId = request.LanguageId
                    }
                }
            };
            //Save image
            if (request.ThumbnailImage != null)
            {
                meal.MealImages = new List<MealImage>()
                {
                    new MealImage()
                    {
                        Caption = "Thumbnail image",
                        DateCreated = DateTime.Now,
                        FileSize = request.ThumbnailImage.Length,
                        ImagePath = await this.SaveFile(request.ThumbnailImage),
                        IsDefault = true,
                        SortOrder = 1
                    }
                };
            }
            _context.Meals.Add(meal);
            await _context.SaveChangesAsync();
            return meal.Id;
        }

        public async Task<int> Delete(int mealId)
        {
            var meal = await _context.Meals.FindAsync(mealId);
            if (meal == null) throw new RSException($"Cannot find a meal: {mealId}");

            var images = _context.MealImages.Where(i => i.MealId == mealId);
            foreach (var image in images)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
            }

            _context.Meals.Remove(meal);

            return await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<MealVm>> GetAllPaging(GetManageMealPagingRequest request)
        {
            //1. Select join
            var query = from p in _context.Meals
                        join pt in _context.MealTranslations on p.Id equals pt.MealId
                        join pic in _context.MealInCategories on p.Id equals pic.MealId into ppic
                        from pic in ppic.DefaultIfEmpty()
                        join c in _context.MealCategories on pic.MealCategoryId equals c.Id into picc
                        from c in picc.DefaultIfEmpty()
                        join pi in _context.MealImages on p.Id equals pi.MealId into ppi
                        from pi in ppi.DefaultIfEmpty()
                        where pt.LanguageId == request.LanguageId && pi.IsDefault == true
                        select new { p, pt, pic, pi };
            //2. filter
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.pt.Name.Contains(request.Keyword));

            if (request.MealCategoryId != null && request.MealCategoryId != 0)
            {
                query = query.Where(p => p.pic.MealCategoryId == request.MealCategoryId);
            }


            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new MealVm()
                {
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    ViewCount = x.p.ViewCount,
                    ThumbnailImage = x.pi.ImagePath
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<MealVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
        }

        public async Task<MealVm> GetById(int mealId, string languageId)
        {
            var meal = await _context.Meals.FindAsync(mealId);
            var mealTranslation = await _context.MealTranslations.FirstOrDefaultAsync(x => x.MealId == mealId
            && x.LanguageId == languageId);

            var mealCategories = await (from c in _context.MealCategories
                                    join ct in _context.MealCategoryTranslations on c.Id equals ct.MealCategoryId
                                    join pic in _context.MealInCategories on c.Id equals pic.MealCategoryId
                                    where pic.MealId == mealId && ct.LanguageId == languageId
                                    select ct.Name).ToListAsync();
            var image = await _context.MealImages.Where(x => x.MealId == mealId && x.IsDefault == true).FirstOrDefaultAsync();
            var mealViewModel = new MealVm()
            {
                Id = meal.Id,
                DateCreated = meal.DateCreated,
                Description = mealTranslation != null ? mealTranslation.Description : null,
                LanguageId = mealTranslation.LanguageId,
                Details = mealTranslation != null ? mealTranslation.Details : null,
                Name = mealTranslation != null ? mealTranslation.Name : null,
                Price = meal.Price,
                SeoAlias = mealTranslation != null ? mealTranslation.SeoAlias : null,
                SeoDescription = mealTranslation != null ? mealTranslation.SeoDescription : null,
                SeoTitle = mealTranslation != null ? mealTranslation.SeoTitle : null,
                ViewCount = meal.ViewCount,
                MealCategories = mealCategories,
                ThumbnailImage = image != null ? image.ImagePath : "no-image.jpg"
            };
            return mealViewModel;
        }
        public async Task<MealImageViewModel> GetImageById(int imageId)
        {
            var image = await _context.MealImages.FindAsync(imageId);
            if (image == null)
                throw new RSException($"Cannot find an image with id {imageId}");

            var viewModel = new MealImageViewModel()
            {
                Caption = image.Caption,
                DateCreated = image.DateCreated,
                FileSize = image.FileSize,
                Id = image.Id,
                ImagePath = image.ImagePath,
                IsDefault = image.IsDefault,
                MealId = image.MealId,
                SortOrder = image.SortOrder
            };
            return viewModel;
        }

        public async Task<List<MealImageViewModel>> GetListImages(int mealId)
        {
            return await _context.MealImages.Where(x => x.MealId == mealId)
                .Select(i => new MealImageViewModel()
                {
                    Caption = i.Caption,
                    DateCreated = i.DateCreated,
                    FileSize = i.FileSize,
                    Id = i.Id,
                    ImagePath = i.ImagePath,
                    IsDefault = i.IsDefault,
                    MealId = i.MealId,
                    SortOrder = i.SortOrder
                }).ToListAsync();
        }

        public async Task<int> RemoveImage(int imageId)
        {
            var mealImage = await _context.MealImages.FindAsync(imageId);
            if (mealImage == null)
                throw new RSException($"Cannot find an image with id {imageId}");
            _context.MealImages.Remove(mealImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(MealUpdateRequest request)
        {
            var meal = await _context.Meals.FindAsync(request.Id);
            var mealTranslations = await _context.MealTranslations.FirstOrDefaultAsync(x => x.MealId == request.Id
            && x.LanguageId == request.LanguageId);

            if (meal == null || mealTranslations == null) throw new RSException($"Cannot find a meal with id: {request.Id}");

            mealTranslations.Name = request.Name;
            mealTranslations.SeoAlias = request.SeoAlias;
            mealTranslations.SeoDescription = request.SeoDescription;
            mealTranslations.SeoTitle = request.SeoTitle;
            mealTranslations.Description = request.Description;
            mealTranslations.Details = request.Details;

            //Save image
            if (request.ThumbnailImage != null)
            {
                var thumbnailImage = await _context.MealImages.FirstOrDefaultAsync(i => i.IsDefault == true && i.MealId == request.Id);
                if (thumbnailImage != null)
                {
                    thumbnailImage.FileSize = request.ThumbnailImage.Length;
                    thumbnailImage.ImagePath = await this.SaveFile(request.ThumbnailImage);
                    _context.MealImages.Update(thumbnailImage);
                }
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateImage(int imageId, MealImageUpdateRequest request)
        {
            var mealImage = await _context.MealImages.FindAsync(imageId);
            if (mealImage == null)
                throw new RSException($"Cannot find an image with id {imageId}");

            if (request.ImageFile != null)
            {
                mealImage.ImagePath = await this.SaveFile(request.ImageFile);
                mealImage.FileSize = request.ImageFile.Length;
            }
            _context.MealImages.Update(mealImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePrice(int mealId, decimal newPrice)
        {
            var meal = await _context.Meals.FindAsync(mealId);
            if (meal == null) throw new RSException($"Cannot find a product with id: {mealId}");
            meal.Price = newPrice;
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }

        public async Task<PagedResult<MealVm>> GetAllByCategoryId(string languageId, GetPublicMealPagingRequest request)
        {
            //1. Select join
            var query = from p in _context.Meals
                        join pt in _context.MealTranslations on p.Id equals pt.MealId
                        join pic in _context.MealInCategories on p.Id equals pic.MealId
                        join c in _context.MealCategories on pic.MealCategoryId equals c.Id
                        where pt.LanguageId == languageId
                        select new { p, pt, pic };
            //2. filter
            if (request.MealCategoryId.HasValue && request.MealCategoryId.Value > 0)
            {
                query = query.Where(p => p.pic.MealCategoryId == request.MealCategoryId);
            }
            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new MealVm()
                {
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    ViewCount = x.p.ViewCount
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<MealVm>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
        }

        public async Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request)
        {
            var user = await _context.Meals.FindAsync(id);
            if (user == null)
            {
                return new ApiErrorResult<bool>($"Meal with id {id} isn't exists");
            }
            foreach (var mealCategory in request.MealCategories)
            {
                var mealInCategory = await _context.MealInCategories
                    .FirstOrDefaultAsync(x => x.MealCategoryId == int.Parse(mealCategory.Id)
                    && x.MealId == id);
                if (mealInCategory != null && mealCategory.Selected == false)
                {
                    _context.MealInCategories.Remove(mealInCategory);
                }
                else if (mealInCategory == null && mealCategory.Selected)
                {
                    await _context.MealInCategories.AddAsync(new MealInCategory()
                    {
                        MealCategoryId = int.Parse(mealCategory.Id),
                        MealId = id
                    });
                }
            }
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<List<MealVm>> GetFeaturedMeals(string languageId, int take)
        {
            //1. Select join
            var query = from p in _context.Meals
                        join pt in _context.MealTranslations on p.Id equals pt.MealId
                        join pic in _context.MealInCategories on p.Id equals pic.MealId into ppic
                        from pic in ppic.DefaultIfEmpty()
                        join pi in _context.MealImages on p.Id equals pi.MealId into ppi
                        from pi in ppi.DefaultIfEmpty()
                        join c in _context.MealCategories on pic.MealCategoryId equals c.Id into picc
                        from c in picc.DefaultIfEmpty()
                        where pt.LanguageId == languageId && (pi == null || pi.IsDefault == true)
                        && p.IsFeatured == true
                        select new { p, pt, pic, pi };

            var data = await query.OrderByDescending(x => x.p.DateCreated).Take(take)
                .Select(x => new MealVm()
                {
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    ViewCount = x.p.ViewCount,
                    ThumbnailImage = x.pi.ImagePath
                }).ToListAsync();

            return data;
        }

        public async Task<List<MealVm>> GetLatestMeals(string languageId, int take)
        {
            //1. Select join
            var query = from p in _context.Meals
                        join pt in _context.MealTranslations on p.Id equals pt.MealId
                        join pic in _context.MealInCategories on p.Id equals pic.MealId into ppic
                        from pic in ppic.DefaultIfEmpty()
                        join pi in _context.MealImages on p.Id equals pi.MealId into ppi
                        from pi in ppi.DefaultIfEmpty()
                        join c in _context.MealCategories on pic.MealCategoryId equals c.Id into picc
                        from c in picc.DefaultIfEmpty()
                        where pt.LanguageId == languageId && (pi == null || pi.IsDefault == true)
                        select new { p, pt, pic, pi };

            var data = await query.OrderByDescending(x => x.p.DateCreated).Take(take)
                .Select(x => new MealVm()
                {
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    ViewCount = x.p.ViewCount,
                    ThumbnailImage = x.pi.ImagePath
                }).ToListAsync();

            return data;
        }
    }
}