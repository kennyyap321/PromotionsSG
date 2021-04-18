using Common.AppSettings;
using Common.DBTableModels;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
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
        #endregion
    }
}
