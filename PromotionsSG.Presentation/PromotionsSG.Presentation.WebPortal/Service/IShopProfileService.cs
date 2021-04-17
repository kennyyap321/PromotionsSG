using Common.DBTableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionsSG.Presentation.WebPortal.Service
{
    public interface IShopProfileService
    {
        #region CRUD
        Task<ShopProfile> RetrieveAsync(int shopProfileId);
        Task<ShopProfile> InsertAsync(ShopProfile shopProfile);
        Task<ShopProfile> UpdateAsync(ShopProfile shopProfile);
        #endregion


        #region Custom
        Task<ShopProfile> RetrieveShopProfileByUserIdAsync(int userId);
        #endregion
    }
}
