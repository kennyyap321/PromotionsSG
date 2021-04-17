using Common.DBTableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionsSG.API.ShopProfileAPI.Repository
{
    public interface IShopProfileRepository
    {
        #region CRUD
        Task<ShopProfile> RetrieveAsync(int shopProfileId);
        Task<ShopProfile> InsertAsync(ShopProfile shopProfile);
        Task<ShopProfile> UpdateAsync(ShopProfile shopProfile);
        #endregion


        #region ShopProfile
        Task<ShopProfile> RetrieveShopProfileByUserIdAsync(int userId);
        #endregion
    }
}
