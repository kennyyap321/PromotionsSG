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

        public async Task<CustomerProfiles> UpdateCustomerProfile(string customerEmail, CustomerProfiles customerProfile)
        {
            string apiURL = URLConfig.CustomerProfile.RetrieveCustomerProfileAPI(_apiUrls.CustomerProfileAPI_Update);
            apiURL += customerEmail;

            var result = await _context.CustomerProfile.FirstOrDefaultAsync(e => e.CustomerEmail == customerProfile.CustomerEmail);

            if (result != null)
            {
                result.CustomerProfileId = customerProfile.CustomerProfileId;
                result.CustomerFullName = customerProfile.CustomerFullName;
                result.CustomerAddress = customerProfile.CustomerAddress;
                result.CustomerEmail = customerProfile.CustomerEmail;
                result.CustomerPhone = customerProfile.CustomerPhone;
                result.CustomerType = customerProfile.CustomerType;
                result.CustomerGender = customerProfile.CustomerGender;
                result.CustomerActive = customerProfile.CustomerActive;
                result.CustomerDOB = customerProfile.CustomerDOB;
                result.CreatedBy = customerProfile.CreatedBy;
                result.CreatedTime = customerProfile.CreatedTime;
                result.LastUpdatedBy = customerProfile.LastUpdatedBy;
                result.LastUpdatedTime = customerProfile.LastUpdatedTime;
                result.VersionNo = customerProfile.VersionNo;
                result.IsDeleted = customerProfile.IsDeleted;

                await _context.SaveChangesAsync();

                return result;
            }

            return null;

        }

        async Task<List<CustomerProfiles>> ICustomerProfileService.GetCustomerProfiles()
        {
            string apiURL = URLConfig.CustomerProfile.RetrieveCustomerProfileAPI(_apiUrls.CustomerProfileAPI_RetrieveAll);
            //apiURL += "?customerFullName=" + customerFullName + "&customerEmail=" + customerEmail;

            var response = await _apiClient.GetStringAsync(apiURL);
            var data = !string.IsNullOrEmpty(response) ? JsonConvert.DeserializeObject<List<CustomerProfiles>>(response) : null;

            return data;
        }
    }
}
