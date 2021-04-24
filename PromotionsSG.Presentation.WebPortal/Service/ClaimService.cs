﻿using Common.AppSettings;
using Common.DBTableModels;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PromotionsSG.Presentation.WebPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PromotionsSG.Presentation.WebPortal.Service
{
    public class ClaimService : IClaimService
    {
        #region Fields
        private readonly HttpClient _httpClient;
        private readonly APIUrls _apiUrls;
        #endregion


        #region Dependency injection
        public ClaimService(HttpClient httpClient, IOptions<APIUrls> apiUrls)
        {
            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
            URLConfig.Claim.BaseURI = _apiUrls.ClaimAPI_Base;
            URLConfig.Promotion.BaseURI = _apiUrls.PromotionAPI_Base;
            URLConfig.ShopProfile.BaseURI = _apiUrls.ShopProfileAPI_Base;
        }
        #endregion


        #region CRUD
        public async Task<Claim> RetrieveAsync(int claimId)
        {
            string apiURL = URLConfig.Claim.ClaimAPI(_apiUrls.ClaimAPI_Retrieve);
            apiURL += "?claimId=" + claimId;

            var response = await _httpClient.GetStringAsync(apiURL);
            var data = !string.IsNullOrEmpty(response) ? JsonConvert.DeserializeObject<Claim>(response) : null;

            return data;
        }

        public async Task<Claim> InsertAsync(Claim claim)
        {
            string apiURL = URLConfig.Claim.ClaimAPI(_apiUrls.ClaimAPI_Insert);
            var payLoad = new StringContent(JsonConvert.SerializeObject(claim), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiURL, payLoad);
            var data = await response.Content.ReadAsAsync<Claim>();

            return data;
        }

        public async Task<Claim> UpdateAsync(Claim claim)
        {
            string apiURL = URLConfig.Claim.ClaimAPI(_apiUrls.ClaimAPI_Update);
            var payLoad = new StringContent(JsonConvert.SerializeObject(claim), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiURL, payLoad);
            var data = await response.Content.ReadAsAsync<Claim>();

            return data;
        }
        #endregion


        #region Custom
        public async Task<Claim> ClaimAsync(Claim claim)
        {
            var apiURL = URLConfig.Claim.ClaimAPI(_apiUrls.ClaimAPI_Claim);
            var payLoad = new StringContent(JsonConvert.SerializeObject(claim), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiURL, payLoad);
            var data = await response.Content.ReadAsAsync<Claim>();

            return data;
        }

        public async Task<IEnumerable<ClaimWithPromotionAndShopInfo>> RetrieveClaimsWithPromotionAndShopInfoByCustomerProfileIdAsync(int customerProfileId)
        {
            //Get list of claims
            string apiUrl = URLConfig.Claim.ClaimAPI(_apiUrls.ClaimAPI_RetrieveByCustomerProfileId);
            apiUrl += "?customerProfileId=" + customerProfileId;

            var response = await _httpClient.GetStringAsync(apiUrl);
            var claims = !string.IsNullOrEmpty(response) ? JsonConvert.DeserializeObject<IEnumerable<Claim>>(response) : null;


            //Get list of promotions
            string apiUrl2 = URLConfig.Promotion.RetrievePromotionsByPromotionIdsAPI(_apiUrls.PromotionAPI_RetrievePromotionsByPromotionIds);
            var payLoad2 = new StringContent(JsonConvert.SerializeObject(claims.Select(c => c.PromotionId)), Encoding.UTF8, "application/json");

            var response2 = await _httpClient.PostAsync(apiUrl2, payLoad2);
            var promotions = await response2.Content.ReadAsAsync<IEnumerable<Promotion>>();


            //Get list of shopProfiles
            string apiUrl3 = URLConfig.ShopProfile.ShopProfileAPI(_apiUrls.ShopProfileAPI_RetrieveShopProfilesByShopProfileIds);
            var payLoad3 = new StringContent(JsonConvert.SerializeObject(promotions.Select(p => p.ShopProfileId)), Encoding.UTF8, "application/json");

            var response3 = await _httpClient.PostAsync(apiUrl3, payLoad3);
            var shopProfiles = await response3.Content.ReadAsAsync<IEnumerable<ShopProfile>>();

            //Combine all into ClaimWithPromotionAndShopInfo
            var claimWithPromotionAndShopInfos = claims.Select(c =>
            {
                var cwpasi = new ClaimWithPromotionAndShopInfo();
                cwpasi.ClaimDto = c;
                cwpasi.PromotionDto = promotions.FirstOrDefault(p => p.PromotionId == c.PromotionId);
                cwpasi.ShopProfileDto = shopProfiles.FirstOrDefault(s => s.ShopProfileId == cwpasi.PromotionDto.ShopProfileId);
                
                return cwpasi;
            });

            return claimWithPromotionAndShopInfos;
        }
        #endregion
    }
}
