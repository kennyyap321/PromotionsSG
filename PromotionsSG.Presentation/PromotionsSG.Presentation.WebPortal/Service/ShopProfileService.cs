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
        private readonly HttpClient _httpClient;
        private readonly APIUrls _apiUrls;

        public ShopProfileService(HttpClient httpClient, IOptions<APIUrls> apiUrls)
        {
            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
            URLConfig.ShopProfile.BaseURI = _apiUrls.ShopProfileAPI_Base;
        }

        #region ShopProfile
        public async Task<ShopProfile> RetrieveShopProfileAsync(int shopProfileId)
        {
            string apiURL = URLConfig.ShopProfile.RetrieveShopProfileAPI(_apiUrls.ShopProfileAPI_Retrieve);
            apiURL += "?shopProfileId=" + shopProfileId;

            var response = await _httpClient.GetStringAsync(apiURL);
            var data = !string.IsNullOrEmpty(response) ? JsonConvert.DeserializeObject<ShopProfile>(response) : null;

            return data;
        }

        public async Task<int> CreateShopProfileAsync(ShopProfile shopProfile)
        {
            string apiURL = URLConfig.ShopProfile.InsertShopProfileAPI(_apiUrls.ShopProfileAPI_Insert);

            var payLoad = new StringContent(JsonConvert.SerializeObject(shopProfile), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiURL, payLoad);

            if (!response.IsSuccessStatusCode)
                return -1;

            var createdShopProfileId = Convert.ToInt32(await response.Content.ReadAsStringAsync());

            return createdShopProfileId;
        }

        public async Task<int> UpdateShopProfileAsync(ShopProfile shopProfile)
        {
            string apiURL = URLConfig.ShopProfile.UpdateShopProfileAPI(_apiUrls.ShopProfileAPI_Update);

            var payLoad = new StringContent(JsonConvert.SerializeObject(shopProfile), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiURL, payLoad);

            if (!response.IsSuccessStatusCode)
                return -1;

            var updatedShopProfileId = shopProfile.UserId;

            return updatedShopProfileId;
        }
        #endregion
    }
}
