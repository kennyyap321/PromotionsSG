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
        private readonly MyDBContext _context;

        public ShopProfileRepository(MyDBContext context)
        {
            _context = context;
        }

        #region ShopProfile
        public async Task<ShopProfile> RetrieveShopProfileByIdAsync(int shopProfileId)
        {
            var result = await _context.ShopProfiles.FirstOrDefaultAsync(s => s.ShopProfileId == shopProfileId);

            return result;
        }

        public async Task<int> CreateShopProfileAsync(ShopProfile shopProfile)
        {
            _context.ShopProfiles.Add(shopProfile);
            var result = await _context.SaveChangesAsync();

            var createdShopProfileId = (await _context.ShopProfiles.FirstAsync(s => s.UserId == shopProfile.UserId)).ShopProfileId;

            return createdShopProfileId;
        }

        public async Task<int> UpdateShopProfileAsync(ShopProfile shopProfile)
        {
            _context.ShopProfiles.Update(shopProfile);
            var result = await _context.SaveChangesAsync();

            var updatedShopProfileId = shopProfile.ShopProfileId;

            return updatedShopProfileId;
        }
        #endregion
    }
}
