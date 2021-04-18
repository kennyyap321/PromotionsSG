using CommonDB = Common.DBTableModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

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

        public async Task<IEnumerable<CommonDB.Promotion>> GetAllPromotions()
        {
            var promotionsData = await _context.Promotions.Where(x => x.IsActive == true).ToListAsync();
            return promotionsData;
        }

        public async Task<IEnumerable<CommonDB.Promotion>> Search(string searchTerm)
        {
            //var query = from m in _context.Promotions
            //             select m;

            //return await query.ToListAsync();

            IQueryable<CommonDB.Promotion> query = _context.Promotions;

            //if (!string.IsNullOrEmpty(searchTerm))
            //{
            //    query = query.Where(s => s.Header.Contains(searchTerm));
            //}

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(s => s.Description.Contains(searchTerm) || s.Header.Contains(searchTerm));
            }
           
            return await query.ToListAsync();
        }
        #endregion
    }
}
