using System.Collections.Generic;
using System.Threading.Tasks;
using CommonDB = Common.DBTableModels;

namespace PromotionsSG.API.PromotionAPI.Repository
{
    public interface IPromotionRepository
    {
        #region Promotion
        Task<CommonDB.Promotion> RetrievePromotionByIdAsync(int promotionId);
        Task<List<CommonDB.Promotion>> RetrievePromotionByShopIdAsync(int shopId); 
        Task<int> CreatePromotionAsync(CommonDB.Promotion promotion);
        Task<CommonDB.Promotion> UpdatePromotionAsync(CommonDB.Promotion promotion);
        Task<CommonDB.Promotion> DeletePromotionAsync(CommonDB.Promotion promotion);
        Task<IEnumerable<CommonDB.Promotion>> GetAllPromotions();
        Task<IEnumerable<CommonDB.Promotion>> Search(string searchTerm);
        Task<IEnumerable<CommonDB.Promotion>> RetrievePromotionByRegionAsync(string region);
        Task<IEnumerable<CommonDB.Promotion>> RetrievePromotionsByPromotionIdsAsync(IEnumerable<int> promotionIds);
        Task<IEnumerable<CommonDB.Promotion>> RetrieveNewestPromotionsAsync();
        #endregion
    }
}
