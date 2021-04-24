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
        Task<ShopProfile> UpdateAsync(ShopProfile shopProfileChanged);
        #endregion


        #region Custom
        Task<ShopProfile> RetrieveShopProfileByUserIdAsync(int userId);
        Task<IEnumerable<ShopProfile>> RetrieveShopProfilesByShopProfileIdsAsync(IEnumerable<int> shopProfileIds);
        #endregion
    }
}
