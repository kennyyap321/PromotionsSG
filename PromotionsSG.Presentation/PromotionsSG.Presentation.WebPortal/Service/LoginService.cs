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
        #region Fields
        private readonly HttpClient _httpClient;
        private readonly APIUrls _apiUrls;
        #endregion


        #region Dependency injection
        public LoginService(HttpClient httpClient, IOptions<APIUrls> apiUrls, IShopProfileService shopProfileService)
        {
            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
            URLConfig.Login.BaseURI = _apiUrls.LoginAPI_Base;
        }
        #endregion


        #region CRUD
        public async Task<User> RetrieveAsync(int userId)
        {
            string apiURL = URLConfig.Login.LoginAPI(_apiUrls.LoginAPI_Retrieve);
            apiURL += "?&userId=" + userId;

            var response = await _httpClient.GetStringAsync(apiURL);
            var data = !string.IsNullOrEmpty(response) ? JsonConvert.DeserializeObject<User>(response) : null;

            return data;
        }

        public async Task<User> InsertAsync(User user)
        {
            string apiURL = URLConfig.Login.LoginAPI(_apiUrls.LoginAPI_Insert);
            var payLoad = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiURL, payLoad);
            var data = await response.Content.ReadAsAsync<User>();

            return data;
        }

        public async Task<User> UpdateAsync(User user)
        {
            string apiURL = URLConfig.Login.LoginAPI(_apiUrls.LoginAPI_Update);
            var payLoad = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiURL, payLoad);
            var data = await response.Content.ReadAsAsync<User>();

            return data;
        }
        #endregion


        #region Custom
        public async Task<User> LoginAsync(int userType, string userName, string password)
        {
            string apiURL = URLConfig.Login.LoginAPI(_apiUrls.LoginAPI_Login);
            var obj = new { userType, userName, password };
            var payLoad = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiURL, payLoad);
            var data = await response.Content.ReadAsAsync<User>();

            return data;
        }

        public async Task<User> RegisterAsync(User user)
        {
            string apiURL = URLConfig.Login.LoginAPI(_apiUrls.LoginAPI_Insert);
            var payLoad = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiURL, payLoad);
            var data = await response.Content.ReadAsAsync<User>();

            return data;
        }
        #endregion
    }
}
