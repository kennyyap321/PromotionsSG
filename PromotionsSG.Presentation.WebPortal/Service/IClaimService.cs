using Common.DBTableModels;
using PromotionsSG.Presentation.WebPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionsSG.Presentation.WebPortal.Service
{
    public interface IClaimService
    {
        #region CRUD
        Task<Claim> RetrieveAsync(int claimId);
        Task<Claim> InsertAsync(Claim claim);
        Task<Claim> UpdateAsync(Claim claim);
        #endregion


        #region Custom
        Task<Claim> ClaimAsync(Claim claim);
        Task<IEnumerable<ClaimWithPromotionAndShopInfo>> RetrieveClaimsWithPromotionAndShopInfoByCustomerProfileIdAsync(int customerProfileId);
        #endregion
    }
}
