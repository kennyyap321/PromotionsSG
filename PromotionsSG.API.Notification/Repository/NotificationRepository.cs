using Common.AppSettings;
using Common.DBTableModels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PromotionsSG.API.NotificationAPI.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        #region Fields
        private readonly HttpClient _httpClient;
        private readonly APIUrls _apiUrls;
        private readonly ILogger<NotificationRepository> _logger;
        #endregion

        #region Dependency injection
        public NotificationRepository(HttpClient httpClient, IOptions<APIUrls> apiUrls, ILogger<NotificationRepository> logger)
        {
            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
            URLConfig.Promotion.BaseURI = _apiUrls.PromotionAPI_Base;
            _logger = logger;
        }
        #endregion

        #region Notification
        public async Task<List<Promotion>> RetrieveTopFiveNewestPromotions()
        {
            _logger.LogInformation("Notification Repo before retrieve promo");
            var promotionList = await RetrieveNewestPromotionsAsync();
            _logger.LogInformation("Notification Repo after retrieve promo");

            return promotionList.Take(5).ToList();
        }
        #endregion
        #region Other api calls
        private async Task<List<Promotion>> RetrieveNewestPromotionsAsync()
        {
            string apiURL = URLConfig.Promotion.RetrievePromotionAPI(_apiUrls.PromotionAPI_RetrieveNewestPromotions);
            _logger.LogInformation("Notification call promo api url: " + apiURL);
            var response = await _httpClient.GetStringAsync(apiURL);
            _logger.LogInformation("Notification call promo api response: " + response);
            var data = !string.IsNullOrEmpty(response) ? JsonConvert.DeserializeObject<List<Promotion>>(response) : null;

            return data;
        }
        #endregion
    }
}
