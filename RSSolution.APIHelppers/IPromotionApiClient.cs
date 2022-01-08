using RSSolution.ViewModels.Catalog.Promotions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.APIHelpers
{
    public interface IPromotionApiClient
    {
        Task<List<PromotionVM>> GetAll();
        Task<PromotionVM> GetById(int id);
        Task<bool> Create(PromotionCreateRequest request);
        Task<bool> Update(PromotionUpdateRequest request);
        Task<bool> Delete(int promotionid);
    }
}