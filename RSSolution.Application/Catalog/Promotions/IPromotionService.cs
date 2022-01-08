using RSSolution.ViewModels.Catalog.Promotions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.Application.Catalog.Promotions
{
    public interface IPromotionService
    {
        Task<int> Create(PromotionCreateRequest request);
        Task<int> Update(PromotionUpdateRequest request);
        Task<int> Delete(int promotionid);
        Task<List<PromotionVM>> GetAll();
        Task<PromotionVM> GetById(int id);
    }
}
