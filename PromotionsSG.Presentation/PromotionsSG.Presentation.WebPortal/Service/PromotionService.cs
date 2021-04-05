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
    public class PromotionService : IPromotionService
    {
        private readonly HttpClient _httpClient;
        private readonly APIUrls _apiUrls;

        public PromotionService(HttpClient httpClient, IOptions<APIUrls> apiUrls)
        {
            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
            URLConfig.Promotion.BaseURI = _apiUrls.PromotionAPI_Base;
        }

        #region Promotion
        public async Task<Promotion> RetrievePromotionByShopIdAsync(int shopId)
        {
            //dev testing
            shopId = 1;
            string apiURL = URLConfig.Promotion.RetrievePromotionByShopIdAPI(_apiUrls.PromotionAPI_RetrieveByShopId);
            apiURL += "?shopId=" + shopId;

            var response = await _httpClient.GetStringAsync(apiURL);
            var data = !string.IsNullOrEmpty(response) ? JsonConvert.DeserializeObject<Promotion>(response) : null;

            return data;
        }

        public async Task<Promotion> RetrievePromotionAsync(int promotionId)
        {
            string apiURL = URLConfig.Promotion.RetrievePromotionAPI(_apiUrls.PromotionAPI_Retrieve);
            apiURL += "?promotionId=" + promotionId;

            var response = await _httpClient.GetStringAsync(apiURL);
            var data = !string.IsNullOrEmpty(response) ? JsonConvert.DeserializeObject<Promotion>(response) : null;

            return data;
        }

        public async Task<int> CreatePromotionAsync(Promotion promotion)
        {
            string apiURL = URLConfig.Promotion.InsertPromotionAPI(_apiUrls.PromotionAPI_Insert);

            var payLoad = new StringContent(JsonConvert.SerializeObject(promotion), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiURL, payLoad);

            if (!response.IsSuccessStatusCode)
                return -1;

            var createdPromotionId = Convert.ToInt32(await response.Content.ReadAsStringAsync());

            return createdPromotionId;
        }

        public async Task<int> UpdatePromotionAsync(Promotion promotion)
        {
            string apiURL = URLConfig.Promotion.UpdatePromotionAPI(_apiUrls.PromotionAPI_Update);

            var payLoad = new StringContent(JsonConvert.SerializeObject(promotion), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiURL, payLoad);

            if (!response.IsSuccessStatusCode)
                return -1;

            var updatedPromotionId = promotion.PromotionId;

            return updatedPromotionId;
        }
        #endregion
    }
}
