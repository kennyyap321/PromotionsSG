using Common.AppSettings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PromotionsSG.Presentation.WebPortal.Service
{
    public class NotificationService : INotificationService
    {
        #region Fields
        private readonly HttpClient _httpClient;
        private readonly APIUrls _apiUrls;
        #endregion


        #region Dependency injection
        public NotificationService(HttpClient httpClient, IOptions<APIUrls> apiUrls)
        {
            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
            URLConfig.Notification.BaseURI = _apiUrls.NotificationAPI_Base;
        }
        #endregion

        #region Custom
        public async Task<bool> SendEmailAsync(Common.DBTableModelsService.EmailService.EmailMessage emailMessage)
        {
            //Get list of claims
            string apiUrl = URLConfig.Notification.SendNotificationAPI(_apiUrls.NotificationAPI_SendEmail);
            var payLoad = new StringContent(JsonConvert.SerializeObject(emailMessage), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, payLoad);
            var data = await response.Content.ReadAsAsync<bool>();
            return data;
            //string apiUrl1 = URLConfig.Notification.SendNotificationAPI(_apiUrls.NotificationAPI_PublishEmail);
            //var response1 = await _httpClient.GetStringAsync(apiUrl1);
            //var data1 = !string.IsNullOrEmpty(response1) && JsonConvert.DeserializeObject<bool>(response1);

            //return data || data1;
        }
        #endregion
    }
}
