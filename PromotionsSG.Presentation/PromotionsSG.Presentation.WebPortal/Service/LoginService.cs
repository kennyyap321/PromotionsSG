using Common.AppSettings;
using Common.DBTableModels;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PromotionsSG.Presentation.WebPortal.Service
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient _apiClient;
        private readonly APIUrls _apiUrls;

        public LoginService(HttpClient serviceClient, IOptions<APIUrls> apiUrls)
        {
            _apiClient = serviceClient;
            _apiUrls = apiUrls.Value;
            URLConfig.Login.BaseURI = _apiUrls.LoginAPI_Base;
        }

        public async Task<UserLogin> Login(string userLoginId, string password)
        {
            string apiURL = URLConfig.Login.RetrieveLoginAPI(_apiUrls.LoginAPI_Retrieve);
            apiURL += "?userLoginId=" + userLoginId + "&password=" + password;
            
            var response = await _apiClient.GetStringAsync(apiURL);
            var data = !string.IsNullOrEmpty(response) ? JsonConvert.DeserializeObject<UserLogin>(response) : null;

            return data;
        }
    }
}
