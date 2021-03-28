using Common.DBTableModels;
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

        public LoginService(HttpClient serviceClient)
        {
            _apiClient = serviceClient;
        }

        public async Task<UserLogin> Login(string userLoginId, string password)
        {
            var response = "";

            var parameter = "?userLoginId=" + userLoginId + "&password=" + password;
            string apiURL = "http://localhost:9999/login" + parameter;
            response = await _apiClient.GetStringAsync(apiURL);
            var data = !string.IsNullOrEmpty(response) ? JsonConvert.DeserializeObject<UserLogin>(response) : null;

            return data;
        }
    }
}
