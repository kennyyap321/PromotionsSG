using Common.AppSettings;
using Common.DBTableModels;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PromotionsSG.Presentation.WebPortal.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PromotionsSG.Presentation.WebPortal.Service
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient _httpClient;
        private readonly IShopProfileService _shopProfileService;
        private readonly APIUrls _apiUrls;

        public LoginService(HttpClient httpClient, IOptions<APIUrls> apiUrls, IShopProfileService shopProfileService)
        {
            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
            URLConfig.Login.BaseURI = _apiUrls.LoginAPI_Base;
            _shopProfileService = shopProfileService;
        }

        #region User
        public async Task<User> LoginAsync(string userName, string password, int userType)
        {
            string apiURL = URLConfig.Login.RetrieveLoginAPI(_apiUrls.LoginAPI_Retrieve);
            apiURL += "?userName=" + userName + "&password=" + password + "&userType=" + userType;

            var response = await _httpClient.GetStringAsync(apiURL);
            var data = !string.IsNullOrEmpty(response) ? JsonConvert.DeserializeObject<User>(response) : null;

            return data;
        }

        public async Task<int> RegisterUserAsync(User user)
        {
            // Create user
            string apiURL = URLConfig.Login.InsertLoginAPI(_apiUrls.LoginAPI_Insert);
            var payLoad = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiURL, payLoad);

            if (!response.IsSuccessStatusCode)
                return -1;

            // Get userId
            var createdUserId = Convert.ToInt32(await response.Content.ReadAsStringAsync());

            // Create shopProfile
            apiURL = URLConfig.ShopProfile.InsertShopProfileAPI(_apiUrls.ShopProfileAPI_Insert);
            ShopProfile shopProfile = new ShopProfile { UserId = createdUserId, ShopName = user.UserName };
            payLoad = new StringContent(JsonConvert.SerializeObject(shopProfile), Encoding.UTF8, "application/json");
            response = await _httpClient.PostAsync(apiURL, payLoad);

            if (!response.IsSuccessStatusCode)
                return -1;

            return createdUserId;
        }

        public async Task<int> UpdateUserAsync(User user)
        {
            string apiURL = URLConfig.Login.UpdateLoginAPI(_apiUrls.LoginAPI_Update);
            var payLoad = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiURL, payLoad);

            if (!response.IsSuccessStatusCode)
                return -1;

            var updatedUserId = user.UserId;

            return updatedUserId;
        }
        #endregion
    }
}
