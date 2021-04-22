using Microsoft.AspNetCore.Mvc;
using PromotionsSG.Presentation.WebPortal.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

namespace PromotionsSG.Presentation.WebPortal.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View("Privacy");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(RegisterViewModel registerViewModel)
        {
            var emailAddress = registerViewModel.Email;
            var sns = new AmazonSimpleNotificationServiceClient();

            if (!string.IsNullOrEmpty(emailAddress))
            {
                var listTopicsRequest = new ListTopicsRequest();
                ListTopicsResponse listTopicsResponse;

                listTopicsResponse = await sns.ListTopicsAsync(listTopicsRequest);
                var selectedTopic = listTopicsResponse.Topics.FirstOrDefault();

                
                await sns.SubscribeAsync(new SubscribeRequest
                {
                    TopicArn = selectedTopic.TopicArn,
                    Protocol = "email",
                    Endpoint = emailAddress
                });
            }
            return View("Index");
        }
    }
}
