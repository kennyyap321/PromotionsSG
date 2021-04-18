using Common.DBTableModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PromotionsSG.Presentation.WebPortal.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using Common.AppSettings;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace PromotionsSG.Presentation.WebPortal.Controllers
{
    public class FeedbackViewController : Controller
    {
        private readonly ILogger<FeedbackViewController> _logger;
        private readonly IFeedbackService _feedbackService;


        public FeedbackViewController(ILogger<FeedbackViewController> logger, IFeedbackService feedbackService)
        {
            _logger = logger;
            _feedbackService = feedbackService;
        }
        [Route("InsertFeedback")]
        public IActionResult InsertFeedback()
        {
            return View();
        }

        [HttpPost]
        [Route("InsertFeedback")]
        public async Task<IActionResult> InsertFeedback(FeedbackViewModel feedbackViewModel)
        {
            Feedbacks feedback = feedbackViewModel.feedbackDto;
            feedback.PromotionId = 2;
            feedback.ClaimId = 3;
            feedback.CreatedTime = DateTime.Now;
            feedback.IsDeleted = false;
            feedback.CustomerId = 2;
            int result = await _feedbackService.CreateFeedback(feedback);

            return View("SuccessFeedbackClick", feedbackViewModel);
        }

        public async Task<IActionResult> SuccessFeedbackClick(FeedbackViewModel feedbackViewModel)
        {
            Feedbacks resultSet = await _feedbackService.Feedback(feedbackViewModel.PromotionId);
            return View("SuccessFeedbackClick", resultSet);
        }

        //public async Task<IActionResult> ViewCustomerProfile()
        //{
        //    var username = HttpContext.Session.GetString("username");
        //    return View(await _customerProfileService.CustomerProfile(username));
        //}


        [HttpGet]
        [Route("UpdateFeedback")]
        public async Task<IActionResult> UpdateFeedback([FromQuery] int promotionId)
        {
            promotionId = 1;//HttpContext.Session.GetString("username");
            Feedbacks feedbacks = await _feedbackService.Feedback(promotionId);
            FeedbackViewModel feedbackViewModel = new FeedbackViewModel { feedbackDto = feedbacks };

            return View("UpdateFeedback", feedbackViewModel);
        }

        [HttpPost]
        [Route("UpdateFeedback")]
        public async Task<IActionResult> UpdateFeedback(FeedbackViewModel feedbackViewModel)
        {
            Feedbacks feedbacks = feedbackViewModel.feedbackDto;
            int result = await _feedbackService.UpdateFeedback(feedbacks);

            return View("SuccessFeedbackClick", feedbackViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
