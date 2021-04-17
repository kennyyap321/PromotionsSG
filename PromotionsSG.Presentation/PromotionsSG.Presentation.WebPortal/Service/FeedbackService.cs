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
    public class FeedbackService : IFeedbackService
    {
        private readonly HttpClient _apiClient;
        private readonly APIUrls _apiUrls;
        private readonly MyDBContext _context;

        public FeedbackService(HttpClient serviceClient, IOptions<APIUrls> apiUrls)
        {
            _apiClient = serviceClient;
            _apiUrls = apiUrls.Value;
            URLConfig.Feedback.BaseURI = _apiUrls.FeedbackAPI_Base;
        }

        public async Task<Feedbacks> Feedback(int promotionId)
        {
            string apiURL = URLConfig.Feedback.RetrieveFeedbackAPI(_apiUrls.FeedbackAPI_Retrieve);
            apiURL += "?&promotionId=" + promotionId;

            var response = await _apiClient.GetStringAsync(apiURL);
            var data = !string.IsNullOrEmpty(response) ? JsonConvert.DeserializeObject<Feedbacks>(response) : null;

            return data;
        }

        async Task<List<Feedbacks>> IFeedbackService.GetFeedbacks()
        {
            string apiURL = URLConfig.Feedback.RetrieveFeedbackAPI(_apiUrls.FeedbackAPI_RetrieveAll);

            var response = await _apiClient.GetStringAsync(apiURL);
            var data = !string.IsNullOrEmpty(response) ? JsonConvert.DeserializeObject<List<Feedbacks>>(response) : null;

            return data;
        }

        public async Task<int> CreateFeedback(Feedbacks feedbacks)
        {
            string apiURL = URLConfig.Feedback.InsertFeedbackAPI(_apiUrls.FeedbackAPI_Insert);

            var payLoad = new StringContent(JsonConvert.SerializeObject(feedbacks), Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(apiURL, payLoad);

            if (!response.IsSuccessStatusCode)
                return -1;

            var createdFeedback = Convert.ToInt32(await response.Content.ReadAsStringAsync());

            return createdFeedback;
        }

        public async Task<int> UpdateFeedback(Feedbacks feedbacks)
        {
            string apiURL = URLConfig.Feedback.UpdateFeedbackAPI(_apiUrls.FeedbackAPI_Update);

            var payLoad = new StringContent(JsonConvert.SerializeObject(feedbacks), Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(apiURL, payLoad);

            if (!response.IsSuccessStatusCode)
                return -1;

            var updatedFeedback = feedbacks.PromotionId;

            return updatedFeedback;
        }
    }
}
