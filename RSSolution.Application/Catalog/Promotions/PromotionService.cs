using Microsoft.EntityFrameworkCore;
using RSSolution.Data.EF;
using RSSolution.Data.Entities;
using RSSolution.Utilities.Exceptions;
using RSSolution.ViewModels.Catalog.Promotions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.Application.Catalog.Promotions
{
    public class PromotionService : IPromotionService
    {
        private readonly RSDbContext _context;
        public PromotionService(RSDbContext context)
        {
            _context = context;
        }
        public async Task<List<PromotionVM>> GetAll()
        {
            var promotion = await _context.Promotions
                 .Select(x => new PromotionVM()
                 {
                     Id = x.Id,                    
                     Code = x.Code,
                     FromDate= x.FromDate,
                     ToDate = x.ToDate,
                     DiscountPercent = x.DiscountPercent,                    

                 }).ToListAsync();
            return promotion;
        }

        public async Task<PromotionVM> GetById(int id)
        {
            var promotion = _context.Promotions.Where(r => r.Id == id).Select(r => new PromotionVM()
            {
                Id = r.Id,
                Code = r.Code,
                FromDate = r.FromDate,
                ToDate = r.ToDate,
                DiscountPercent = r.DiscountPercent,
            }).FirstOrDefault();

            return promotion;
        }
        public async Task<int> Create(PromotionCreateRequest request)
        {
            if (request == null)
                throw new Exception("Promotion cannot be null");
            var promotion = new Data.Entities.Promotions()
            {
                Id = request.Id,
                Code = request.Code,
                FromDate = request.FromDate,
                ToDate = request.ToDate,
                DiscountPercent = request.DiscountPercent,
            };
            _context.Promotions.Add(promotion);
            await _context.SaveChangesAsync();
            return promotion.Id;
        }
        public async Task<int> Update(PromotionUpdateRequest request)
        {
            var promotion = await _context.Promotions.FindAsync(request.Id);
            promotion.Code = request.Code;
            promotion.FromDate = request.FromDate;
            promotion.ToDate = request.ToDate;
            promotion.DiscountPercent = request.DiscountPercent;
            return await _context.SaveChangesAsync();
        }
        public async Task<int> Delete(int promotionid)
        {
            var promotion = await _context.Promotions.FindAsync(promotionid);
            if (promotion == null) throw new RSException($"Cannot find a table: {promotionid}");

            _context.Promotions.Remove(promotion);

            return await _context.SaveChangesAsync();
        }
    }
}
