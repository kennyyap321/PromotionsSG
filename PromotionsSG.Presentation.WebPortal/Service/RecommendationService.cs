using Common.AppSettings;
using Common.DBTableModels;
using Microsoft.EntityFrameworkCore;
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
    public class RecommendationService : IRecommendationService
    {
        private readonly HttpClient _apiClient;
        private readonly APIUrls _apiUrls;
        private readonly MyDBContext _context;

        public RecommendationService(HttpClient serviceClient, IOptions<APIUrls> apiUrls)
        {
            _apiClient = serviceClient;
            _apiUrls = apiUrls.Value;
            URLConfig.Recommendation.BaseURI = _apiUrls.RecommendationAPI_Base;
        }

        //public async Task<CustomerProfiles> CustomerProfile(string customerEmail)
        //{
        //    string apiURL = URLConfig.CustomerProfile.RetrieveCustomerProfileAPI(_apiUrls.CustomerProfileAPI_Retrieve);
        //    apiURL += "?&customerEmail=" + customerEmail;

        //    var response = await _apiClient.GetStringAsync(apiURL);
        //    var data = !string.IsNullOrEmpty(response) ? JsonConvert.DeserializeObject<CustomerProfiles>(response) : null;

        //    return data;
        //}

        //public async Task<string> CreateCustomer(CustomerProfiles customer)
        //{
        //    string apiURL = URLConfig.CustomerProfile.InsertCustomerProfileAPI(_apiUrls.CustomerProfileAPI_Insert);

        //    var payLoad = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
        //    var response = await _apiClient.PostAsync(apiURL, payLoad);

        //    if (!response.IsSuccessStatusCode)
        //        return "Success";

        //    var createdCustomerEmail = Convert.ToString(await response.Content.ReadAsStringAsync());

        //    return createdCustomerEmail;
        //}

        //public async Task<string> UpdateCustomer(CustomerProfiles customerProfiles)
        //{
        //    string apiURL = URLConfig.CustomerProfile.UpdateCustomerAPI(_apiUrls.CustomerProfileAPI_Update);

        //    var payLoad = new StringContent(JsonConvert.SerializeObject(customerProfiles), Encoding.UTF8, "application/json");
        //    var response = await _apiClient.PostAsync(apiURL, payLoad);

        //    if (!response.IsSuccessStatusCode)
        //        return "Success";

        //    var updatedCustomerEmail = customerProfiles.CustomerEmail;

        //    return updatedCustomerEmail;
        //}

        #region Get Region By Postal Code
        public async Task<List<CodeTable>> GetCentralRegionByPostalCode()
        {
            string apiURL = URLConfig.Recommendation.RetrieveAllCentralRegionByPostalCodeAPI(_apiUrls.RecommendationAPI_RetrieveCentralPostalCode);

            var response = await _apiClient.GetAsync(apiURL);
            var data = await response.Content.ReadAsStringAsync();
            var jsonString = !string.IsNullOrEmpty(data) ? JsonConvert.DeserializeObject<List<CodeTable>>(data) : null;

            return jsonString;
        }

        public async Task<List<CodeTable>> GetEastRegionByPostalCode()
        {
            string apiURL = URLConfig.Recommendation.RetrieveAllEastRegionByPostalCodeAPI(_apiUrls.RecommendationAPI_RetrieveEastPostalCode);

            var response = await _apiClient.GetAsync(apiURL);
            var data = await response.Content.ReadAsStringAsync();
            var jsonString = !string.IsNullOrEmpty(data) ? JsonConvert.DeserializeObject<List<CodeTable>>(data) : null;

            return jsonString;
        }

        public async Task<List<CodeTable>> GetNorthRegionByPostalCode()
        {
            string apiURL = URLConfig.Recommendation.RetrieveAllNorthRegionByPostalCodeAPI(_apiUrls.RecommendationAPI_RetrieveNorthPostalCode);

            var response = await _apiClient.GetAsync(apiURL);
            var data = await response.Content.ReadAsStringAsync();
            var jsonString = !string.IsNullOrEmpty(data) ? JsonConvert.DeserializeObject<List<CodeTable>>(data) : null;

            return jsonString;
        }


        public async Task<List<CodeTable>> GetNERegionByPostalCode()
        {
            string apiURL = URLConfig.Recommendation.RetrieveAllNERegionByPostalCodeAPI(_apiUrls.RecommendationAPI_RetrieveNEPostalCode);

            var response = await _apiClient.GetAsync(apiURL);
            var data = await response.Content.ReadAsStringAsync();
            var jsonString = !string.IsNullOrEmpty(data) ? JsonConvert.DeserializeObject<List<CodeTable>>(data) : null;

            return jsonString;
        }

        public async Task<List<CodeTable>> GetWestRegionByPostalCode()
        {
            string apiURL = URLConfig.Recommendation.RetrieveAllWestRegionByPostalCodeAPI(_apiUrls.RecommendationAPI_RetrieveWestPostalCode);

            var response = await _apiClient.GetAsync(apiURL);
            var data = await response.Content.ReadAsStringAsync();
            var jsonString = !string.IsNullOrEmpty(data) ? JsonConvert.DeserializeObject<List<CodeTable>>(data) : null;

            return jsonString;
        }

        #endregion
    }
}
