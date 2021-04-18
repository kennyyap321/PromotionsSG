using Common.DBTableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionsSG.API.ClaimAPI.Repository
{
    public interface IClaimRepository
    {
        #region CRUD
        Task<Claim> RetrieveAsync(int claimId);
        Task<IEnumerable<Claim>> RetrieveAllAsync();
        Task<Claim> InsertAsync(Claim claim);
        Task<Claim> UpdateAsync(Claim claimChanged);
        #endregion


        #region Custom
        Task<Claim> ClaimAsync(Claim claim);
        #endregion
    }
}
