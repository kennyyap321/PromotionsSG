using Common.DBTableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionsSG.API.ShopProfileAPI.Repository
{
    public interface IShopProfileRepository
    {
        #region ShopProfile
        Task<ShopProfile> RetrieveShopProfileByIdAsync(int shopProfileId);
        Task<int> CreateShopProfileAsync(ShopProfile shopProfile);
        Task<int> UpdateShopProfileAsync(ShopProfile shopProfile);
        #endregion
    }
}
