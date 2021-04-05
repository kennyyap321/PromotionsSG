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
        #endregion
    }
}
