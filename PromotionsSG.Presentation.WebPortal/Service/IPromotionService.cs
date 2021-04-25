using Common.DBTableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionsSG.Presentation.WebPortal.Service
{
    public interface IPromotionService
    {
        #region Promotion
        Task<Promotion> RetrievePromotionAsync(int promotionId);
        Task<Promotion> RetrievePromotionByShopIdAsync(int shopId);
        Task<int> CreatePromotionAsync(Promotion promotion);
        Task<int> UpdatePromotionAsync(Promotion promotion);
        Task<List<Promotion>> GetAllPromotions();
        Task<List<Promotion>> Search(string searchTerm);
        Task<List<Promotion>> RetrievePromotionByRegionAsync(string region);
        #endregion
    }
}
