using Common.DBTableModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PromotionsSG.Presentation.WebPortal.Models;
using PromotionsSG.Presentation.WebPortal.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Http;

namespace PromotionsSG.Presentation.WebPortal.Controllers
{
    public class RecommendationViewController : Controller
    {
        private readonly ILogger<RecommendationViewController> _logger;
        private readonly IRecommendationService _recommendationService;
        private readonly ICustomerProfileService _customerProfileService;
        private readonly IPromotionService _promotionService;

        public RecommendationViewController(ILogger<RecommendationViewController> logger, IRecommendationService recommendationService, ICustomerProfileService customerProfileService, IPromotionService promotionService)
        {
            _logger = logger;
            _recommendationService = recommendationService;
            _customerProfileService = customerProfileService;
            _promotionService = promotionService;
        }

        #region Recommendation
        public async Task<IActionResult> ViewRecommendation()
        {

            var username = HttpContext.Session.GetString("username");
            return View(await _customerProfileService.CustomerProfile(username));
        }

        [HttpGet]
        [Route("/PromotionByRegion")]
        public async Task<IActionResult> ViewRecommendation(RecommendationViewModel recommendationViewModels)
        {
            var username = HttpContext.Session.GetString("username");

            var customerData = await _customerProfileService.CustomerProfile(username);

            List<Promotion> promotionsbyregion = await _promotionService.RetrievePromotionByRegionAsync(customerData.Region);

            //new promotions
            List<Promotion> allnewpromotions = await _promotionService.GetAllPromotions();

            if (promotionsbyregion == null || promotionsbyregion.Count < 1 || customerData.Region == null || allnewpromotions == null)
            {
                //to return empty if no results returned
                ViewBag.PromotionStatus = "No promotions available";
                return View();
            }

            var promotionViewModel = new RecommendationViewModel();
            promotionViewModel.AllPromotionsByRegion = promotionsbyregion;
            promotionViewModel.AllNewPromotions = allnewpromotions;


            //RecommendationViewModel promotionViewModel = new RecommendationViewModel { AllPromotionsByRegion = promotionsbyregion };

            return View(promotionViewModel);
        }
        #endregion


        #region Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}
