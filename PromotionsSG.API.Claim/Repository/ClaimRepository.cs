using Common.DBTableModels;
using PromotionsSG.API.ClaimAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionsSG.API.ClaimAPI.Repository
{
    public class ClaimRepository : IClaimRepository
    {
        #region Fields
        private readonly MyDBContext _context;
        #endregion


        #region Dependency injection
        public ClaimRepository(MyDBContext context)
        {
            _context = context;
        }
        #endregion


        #region CRUD
        public async Task<Claim> RetrieveAsync(int claimId)
        {
            return await _context.Claims.FindAsync(claimId);
        }

        public async Task<IEnumerable<Claim>> RetrieveAllAsync()
        {
            return _context.Claims;
        }

        public async Task<Claim> InsertAsync(Claim claim)
        {
            _context.Claims.Add(claim);
            await _context.SaveChangesAsync();

            return claim;
        }

        public async Task<Claim> UpdateAsync(Claim claimChanged)
        {
            var claim = _context.Claims.Attach(claimChanged);
            claim.State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return claimChanged;
        }
        #endregion


        #region Custom
        #endregion
    }
}
