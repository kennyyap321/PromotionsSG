using System.Collections.Generic;
using System.Threading.Tasks;
using CommonDB = Common.DBTableModels;

namespace PromotionsSG.API.PromotionAPI.Repository
{
    public interface IPromotionRepository
    {
        #region Promotion
        Task<CommonDB.Promotion> RetrievePromotionByIdAsync(int promotionId);
        Task<CommonDB.Promotion> RetrievePromotionByShopIdAsync(int shopId);
        Task<int> CreatePromotionAsync(CommonDB.Promotion promotion);
        Task<int> UpdatePromotionAsync(CommonDB.Promotion promotion);
        Task<IEnumerable<CommonDB.Promotion>> GetAllPromotions();
        Task<IEnumerable<CommonDB.Promotion>> Search(string searchTerm);
        Task<IEnumerable<CommonDB.Promotion>> RetrievePromotionsByPromotionIdsAsync(IEnumerable<int> promotionIds);
        #endregion
    }
}
