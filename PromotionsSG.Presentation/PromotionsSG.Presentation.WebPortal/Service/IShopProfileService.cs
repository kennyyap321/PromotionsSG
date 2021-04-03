using Common.DBTableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionsSG.Presentation.WebPortal.Service
{
    public interface IShopProfileService
    {
        #region ShopProfile
        Task<ShopProfile> RetrieveShopProfileAsync(int shopProfileId);
        Task<int> CreateShopProfileAsync(ShopProfile shopProfile);
        Task<int> UpdateShopProfileAsync(ShopProfile shopProfile);
        #endregion
    }
}
