using Common.DBTableModels;
using PromotionsSG.API.ShopProfileAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionsSG.API.ShopProfileAPI.Repository
{
    public class ShopProfileRepository : IShopProfileRepository
    {
        #region Fields
        private readonly MyDBContext _context;
        #endregion


        #region Dependency injection
        public ShopProfileRepository(MyDBContext context)
        {
            _context = context;
        }
        #endregion


        #region CRUD
        public async Task<ShopProfile> RetrieveAsync(int shopProfileId)
        {
            return await _context.ShopProfiles.FindAsync(shopProfileId);
        }

        public async Task<ShopProfile> InsertAsync(ShopProfile shopProfile)
        {
            if (_context.ShopProfiles.FirstOrDefault(p => p.UserId == shopProfile.UserId) == null)
            {
                _context.ShopProfiles.Add(shopProfile);
                await _context.SaveChangesAsync();

                return shopProfile;
            }

            return null;
        }

        public async Task<ShopProfile> UpdateAsync(ShopProfile shopProfileChanged)
        {
            var shopProfile = _context.ShopProfiles.Attach(shopProfileChanged);
            shopProfile.State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return shopProfileChanged;
        }
        #endregion


        #region Custom
        public async Task<ShopProfile> RetrieveShopProfileByUserIdAsync(int userId)
        {
            return await _context.ShopProfiles.FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<IEnumerable<ShopProfile>> RetrieveShopProfilesByShopProfileIdsAsync(IEnumerable<int> shopProfileIds)
        {
            return _context.ShopProfiles.Where(s => shopProfileIds.Contains(s.ShopProfileId));
        }
        #endregion
    }
}
