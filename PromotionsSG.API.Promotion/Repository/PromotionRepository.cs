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
        public async Task<List<CommonDB.Promotion>> RetrievePromotionByShopIdAsync(int shopId)
        {
            var result = await _context.Promotions.Where(s => s.ShopProfileId == shopId && s.IsActive).ToListAsync();

            return result;
        }

        public async Task<CommonDB.Promotion> RetrievePromotionByIdAsync(int promotionId)
        {
            var result = await _context.Promotions.FirstOrDefaultAsync(s => s.PromotionId == promotionId);

            return result;
        }

        public async Task<IEnumerable<CommonDB.Promotion>> RetrievePromotionByRegionAsync(string region)
        {
            var promotionsData = await _context.Promotions.Where(x => x.Region == region).ToListAsync();
            return promotionsData;
        }

        public async Task<int> CreatePromotionAsync(CommonDB.Promotion promotion)
        {
            _context.Promotions.Add(promotion);
            var result = await _context.SaveChangesAsync();

            var createdPromotionId = (await _context.Promotions.FirstAsync(s => s.PromotionId == promotion.PromotionId)).PromotionId;

            return createdPromotionId;
        }

        public async Task<CommonDB.Promotion> UpdatePromotionAsync(CommonDB.Promotion promotion)
        {
            var promotionToBeUpdated = _context.Promotions.Attach(promotion);
            promotionToBeUpdated.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return promotion;
        }

        public async Task<CommonDB.Promotion> DeletePromotionAsync(CommonDB.Promotion promotion)
        {
            var promotionToBeUpdated = _context.Promotions.Attach(promotion);
            promotionToBeUpdated.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return promotion;
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

        public async Task<IEnumerable<CommonDB.Promotion>> RetrievePromotionsByPromotionIdsAsync(IEnumerable<int> promotionIds)
        {
            var result = _context.Promotions.Where(p => promotionIds.Contains(p.PromotionId));

            return result;
        }

        public async Task<IEnumerable<CommonDB.Promotion>> RetrieveNewestPromotionsAsync()
        {
            var result = _context.Promotions.Where(p => p.StartDate >= DateTime.Today.AddDays(-1)).OrderByDescending(p => p.PromotionId);

            return result;
        }
        #endregion
    }
}
