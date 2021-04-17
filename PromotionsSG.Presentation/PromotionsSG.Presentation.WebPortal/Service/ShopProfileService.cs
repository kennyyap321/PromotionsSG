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
    public class ShopProfileService : IShopProfileService
    {
        #region Fields
        private readonly HttpClient _httpClient;
        private readonly APIUrls _apiUrls;
        #endregion


        #region Dependency injection
        public ShopProfileService(HttpClient httpClient, IOptions<APIUrls> apiUrls)
        {
            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
            URLConfig.ShopProfile.BaseURI = _apiUrls.ShopProfileAPI_Base;
        }
        #endregion


        #region CRUD
        public async Task<ShopProfile> RetrieveAsync(int shopProfileId)
        {
            string apiURL = URLConfig.ShopProfile.ShopProfileAPI(_apiUrls.ShopProfileAPI_Retrieve);
            apiURL += "?shopProfileId=" + shopProfileId;

            var response = await _httpClient.GetStringAsync(apiURL);
            var data = !string.IsNullOrEmpty(response) ? JsonConvert.DeserializeObject<ShopProfile>(response) : null;

            return data;
        }

        public async Task<ShopProfile> InsertAsync(ShopProfile shopProfile)
        {
            string apiURL = URLConfig.ShopProfile.ShopProfileAPI(_apiUrls.ShopProfileAPI_Insert);
            var payLoad = new StringContent(JsonConvert.SerializeObject(shopProfile), Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync(apiURL, payLoad);
            var data = await response.Content.ReadAsAsync<ShopProfile>();

            return data;
        }

        public async Task<ShopProfile> UpdateAsync(ShopProfile shopProfile)
        {
            string apiURL = URLConfig.ShopProfile.ShopProfileAPI(_apiUrls.ShopProfileAPI_Update);
            var payLoad = new StringContent(JsonConvert.SerializeObject(shopProfile), Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync(apiURL, payLoad);
            var data = await response.Content.ReadAsAsync<ShopProfile>();

            return data;
        }
        #endregion


        #region Custom
        public async Task<ShopProfile> RetrieveShopProfileByUserIdAsync(int userId)
        {
            string apiURL = URLConfig.ShopProfile.ShopProfileAPI(_apiUrls.ShopProfileAPI_RetrieveShopProfileByUserId);
            apiURL += "?userId=" + userId;

            var response = await _httpClient.GetStringAsync(apiURL);
            var data = !string.IsNullOrEmpty(response) ? JsonConvert.DeserializeObject<ShopProfile>(response) : null;

            return data;
        }
        #endregion
    }
}
