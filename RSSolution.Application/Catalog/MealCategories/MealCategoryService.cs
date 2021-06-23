using RSSolution.Data.EF;
using RSSolution.ViewModels.Catalog.MealCategories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RSSolution.Data.Entities;
using RSSolution.Utilities.Constants;
using RSSolution.Utilities.Exceptions;

namespace RSSolution.Application.Catalog.MealCategories
{
    public class MealCategoryService : IMealCategoryService
    {
        private readonly RSDbContext _context;

        public MealCategoryService(RSDbContext context)
        {
            _context = context;
        }

        public async Task<List<MealCategoryVm>> GetAll(string languageId)
        {
            var query = from c in _context.MealCategories
                        join ct in _context.MealCategoryTranslations on c.Id equals ct.MealCategoryId
                        where ct.LanguageId == languageId
                        select new { c, ct };
            return await query.Select(x => new MealCategoryVm()
            {
                Id = x.c.Id,
                Name = x.ct.Name,
                ParentId = x.c.ParentId
            }).ToListAsync();
        }

        public async Task<MealCategoryVm> GetById(string languageId, int id)
        {
            var query = from c in _context.MealCategories
                        join ct in _context.MealCategoryTranslations on c.Id equals ct.MealCategoryId
                        where ct.LanguageId == languageId && c.Id == id
                        select new { c, ct };
            return await query.Select(x => new MealCategoryVm()
            {
                Id = x.c.Id,
                Name = x.ct.Name,
                ParentId = x.c.ParentId
            }).FirstOrDefaultAsync();
        }
        public async Task<int> CreateMealCategory(MealCategoryCreateRequest request)
        {
            var languages = _context.Languages;
            var translations = new List<MealCategoryTranslation>();
            foreach (var language in languages)
            {
                if (language.Id == request.LanguageId)
                {
                    translations.Add(new MealCategoryTranslation()
                    {
                        Name = request.Name,
             
                        SeoDescription = request.SeoDescription,
                        SeoAlias = request.SeoAlias,
                        SeoTitle = request.SeoTitle,
                        LanguageId = request.LanguageId
                    });
                }
                else
                {
                    translations.Add(new MealCategoryTranslation()
                    {
                        Name = SystemConstants.MealConstants.NA,
                        SeoAlias = SystemConstants.MealConstants.NA,
                        LanguageId = language.Id
                    });
                }
            }
            var mealCategory = new MealCategory()
            {
                SortOrder = request.SortOrder,
                ParentId = null,               
                MealCategoryTranslations = translations,                
            };
            _context.MealCategories.Add(mealCategory);
            await _context.SaveChangesAsync();
            return mealCategory.Id;
        }
        public async Task<int> DeleteMealCategory(int mealCategoryId)
        {
            var mealCategory = await _context.MealCategories.FindAsync(mealCategoryId);
            if (mealCategory == null) throw new RSException($"Cannot find a meal category: {mealCategoryId}");

            _context.MealCategories.Remove(mealCategory);

            return await _context.SaveChangesAsync();
        }
        public async Task<int> UpdateMealCategory(MealCategoryUpdateRequest request)
        {
            var mealCategory = await _context.MealCategories.FindAsync(request.Id);
            var mealCategoryTranslations = await _context.MealCategoryTranslations.FirstOrDefaultAsync(x => x.MealCategoryId == request.Id
            && x.LanguageId == request.LanguageId);

            if (mealCategory == null || mealCategoryTranslations == null) throw new RSException($"Cannot find a meal category with id: {request.Id}");

            mealCategoryTranslations.Name = request.Name;
            mealCategoryTranslations.SeoAlias = request.SeoAlias;
            mealCategoryTranslations.SeoDescription = request.SeoDescription;
            mealCategoryTranslations.SeoTitle = request.SeoTitle;

            return await _context.SaveChangesAsync();
        }
    }
}