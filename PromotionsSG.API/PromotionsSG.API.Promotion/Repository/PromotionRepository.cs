using CommonDB = Common.DBTableModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace PromotionsSG.API.PromotionAPI.Repository
{
    public class PromotionRepository : IPromotionRepository
    {
        private readonly CommonDB.MyDBContext _context;

        public PromotionRepository(CommonDB.MyDBContext context)
        {
            _context = context;
        }

        #region Promotion
        public async Task<CommonDB.Promotion> RetrievePromotionByShopIdAsync(int shopId)
        {
            var result = await _context.Promotions.FirstOrDefaultAsync(s => s.ShopProfileId == shopId);

            return result;
        }

        public async Task<CommonDB.Promotion> RetrievePromotionByIdAsync(int promotionId)
        {
            var result = await _context.Promotions.FirstOrDefaultAsync(s => s.PromotionId == promotionId);

            return result;
        }

        public async Task<int> CreatePromotionAsync(CommonDB.Promotion promotion)
        {
            _context.Promotions.Add(promotion);
            var result = await _context.SaveChangesAsync();

            var createdPromotionId = (await _context.Promotions.FirstAsync(s => s.PromotionId == promotion.PromotionId)).PromotionId;

            return createdPromotionId;
        }

        public async Task<int> UpdatePromotionAsync(CommonDB.Promotion promotion)
        {
            _context.Promotions.Update(promotion);
            var result = await _context.SaveChangesAsync();

            var updatedPromotionId = promotion.PromotionId;

            return updatedPromotionId;
        }
        #endregion
    }
}
