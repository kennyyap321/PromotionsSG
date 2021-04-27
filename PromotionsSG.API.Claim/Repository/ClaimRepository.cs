using Common.DBTableModels;
using PromotionsSG.API.ClaimAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Common.AppSettings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace PromotionsSG.API.ClaimAPI.Repository
{
    public class ClaimRepository : IClaimRepository
    {
        #region Fields
        private readonly MyDBContext _context;
        private readonly HttpClient _httpClient;
        private readonly APIUrls _apiUrls;
        #endregion


        #region Dependency injection
        public ClaimRepository(MyDBContext context, HttpClient httpClient, IOptions<APIUrls> apiUrls)
        {
            _context = context;
            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
            URLConfig.Login.BaseURI = _apiUrls.LoginAPI_Base;
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
        public async Task<Claim> ClaimAsync(Claim claim)
        {
            var promotion = await RetrievePromotionForClaimAsync(claim.PromotionId);

            promotion.Qty -= 1;
            await UpdatePromotionForClaimAsync(promotion);

            claim.ClaimDate = DateTime.Now;

            _context.Claims.Add(claim);
            await _context.SaveChangesAsync();

            return claim;
        }

        public async Task<IEnumerable<Claim>> RetrieveByCustomerProfileIdAsync(int customerProfileId)
        {
            return _context.Claims.Where(c => c.CustomerProfileId == customerProfileId);
        }

        public async Task<IEnumerable<Claim>> RetrieveClaimByPromotionAsync()
        {
            return _context.Claims.GroupBy(c => c.PromotionId).Select(tl => new Claim { PromotionId = tl.Key, TotalClaim = tl.Count() }).AsEnumerable();
        }

        #endregion


        #region Other api calls
        private async Task<Promotion> RetrievePromotionForClaimAsync(int promotionId)
        {
            string apiURL = URLConfig.Promotion.RetrievePromotionAPI(_apiUrls.PromotionAPI_Retrieve);
            apiURL += "?&promotionId=" + promotionId;

            var response = await _httpClient.GetStringAsync(apiURL);
            var data = !string.IsNullOrEmpty(response) ? JsonConvert.DeserializeObject<Promotion>(response) : null;

            return data;
        }

        private async Task<Promotion> UpdatePromotionForClaimAsync(Promotion promotion)
        {
            string apiURL = URLConfig.Promotion.UpdatePromotionAPI(_apiUrls.PromotionAPI_Update);
            var payLoad = new StringContent(JsonConvert.SerializeObject(promotion), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiURL, payLoad);
            var data = JsonConvert.DeserializeObject<int>(await response.Content.ReadAsStringAsync());

            return promotion;
        }
        #endregion
    }
}
