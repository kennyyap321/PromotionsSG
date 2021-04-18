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
        public async Task<Claim> ClaimAsync(int promotionId, string userName)
        {
            string apiURL = URLConfig.CustomerProfile.RetrieveCustomerProfileAPI(_apiUrls.CustomerProfileAPI_Retrieve);
            apiURL += "?&customerEmail=" + userName.Replace("@","%40");

            //Todo: Get customerProfileId
            //var response = await _httpClient.GetStringAsync(apiURL);
            //var data = !string.IsNullOrEmpty(response) ? JsonConvert.DeserializeObject<CustomerProfiles>(response) : null;
            
            Claim claim = new Claim { PromotionId = promotionId, CustomerProfileId = 1 };
                        
            var apiURL2 = URLConfig.Claim.ClaimAPI(_apiUrls.ClaimAPI_Claim);
            var payLoad2 = new StringContent(JsonConvert.SerializeObject(claim), Encoding.UTF8, "application/json");

            var response2 = await _httpClient.PostAsync(apiURL2, payLoad2);
            var data2 = await response2.Content.ReadAsAsync<Claim>();

            return data2;
        }
        #endregion
    }
}
