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
    public class CustomerProfileService : ICustomerProfileService
    {
        private readonly HttpClient _apiClient;
        private readonly APIUrls _apiUrls;
        private readonly MyDBContext _context;

        public CustomerProfileService(HttpClient serviceClient, IOptions<APIUrls> apiUrls)
        {
            _apiClient = serviceClient;
            _apiUrls = apiUrls.Value;
            URLConfig.CustomerProfile.BaseURI = _apiUrls.CustomerProfileAPI_Base;
        }

        public async Task<CustomerProfiles> CustomerProfile(string customerEmail)
        {
            string apiURL = URLConfig.CustomerProfile.RetrieveCustomerProfileAPI(_apiUrls.CustomerProfileAPI_Retrieve);
            apiURL += "?&customerEmail=" + customerEmail;

            var response = await _apiClient.GetStringAsync(apiURL);
            var data = !string.IsNullOrEmpty(response) ? JsonConvert.DeserializeObject<CustomerProfiles>(response) : null;

            return data;
        }

        async Task<List<CustomerProfiles>> ICustomerProfileService.GetCustomerProfiles()
        {
            string apiURL = URLConfig.CustomerProfile.RetrieveCustomerProfileAPI(_apiUrls.CustomerProfileAPI_RetrieveAll);
            //apiURL += "?customerFullName=" + customerFullName + "&customerEmail=" + customerEmail;

            var response = await _apiClient.GetStringAsync(apiURL);
            var data = !string.IsNullOrEmpty(response) ? JsonConvert.DeserializeObject<List<CustomerProfiles>>(response) : null;

            return data;
        }

        public async Task<string> CreateCustomer(CustomerProfiles customer)
        {
            string apiURL = URLConfig.CustomerProfile.InsertCustomerProfileAPI(_apiUrls.CustomerProfileAPI_Insert);

            var payLoad = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(apiURL, payLoad);

            if (!response.IsSuccessStatusCode)
                return "Success";

            var createdCustomerEmail = Convert.ToString(await response.Content.ReadAsStringAsync());

            return createdCustomerEmail;
        }

        public async Task<string> UpdateCustomer(CustomerProfiles customerProfiles)
        {
            string apiURL = URLConfig.CustomerProfile.UpdateCustomerAPI(_apiUrls.CustomerProfileAPI_Update);

            var payLoad = new StringContent(JsonConvert.SerializeObject(customerProfiles), Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(apiURL, payLoad);

            if (!response.IsSuccessStatusCode)
                return "Success";

            var updatedCustomerEmail = customerProfiles.CustomerEmail;

            return updatedCustomerEmail;
        }
    }
}
