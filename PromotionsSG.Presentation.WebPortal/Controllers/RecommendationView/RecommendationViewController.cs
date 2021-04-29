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
using System.Linq;

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
        [Route("/PromotionByRecommendation")]
        public async Task<IActionResult> ViewRecommendation(RecommendationViewModel recommendationViewModels)
        {
            var username = HttpContext.Session.GetString("username");

            var customerData = await _customerProfileService.CustomerProfile(username);

            List<Promotion> promotionsbyregion = await _promotionService.RetrievePromotionByRegionAsync(customerData.Region);

            //to add recommendation based on new promotions
            IEnumerable<Promotion> allnewpromotions = await _promotionService.RetrieveNewestPromotionsAsync();

            //popular promotions
            List<Claim> allpopularpromotions = await _recommendationService.GetClaimCountByPromotion();

            var promotionViewModel = new RecommendationViewModel();

            int mostClaimed = allpopularpromotions.Count > 0 ? allpopularpromotions.Max(s => s.TotalClaim) : 0;

            var ItemsList = new List<Promotion>();

            foreach (var claimedpromo in allpopularpromotions)
            {
                if (claimedpromo.TotalClaim == mostClaimed)
                {
                    var toppromo = await _promotionService.RetrievePromotionAsync(claimedpromo.PromotionId);

                    ItemsList.Add(new Promotion { PromotionId = toppromo.PromotionId, ShopProfileId = toppromo.ShopProfileId, Description = toppromo.Description, EndDate = toppromo.EndDate, Header = toppromo.Header, IsActive = toppromo.IsActive, Qty = toppromo.Qty, Region = toppromo.Region, StartDate = toppromo.StartDate, Type = toppromo.Type });

                }
            }

            promotionViewModel.AllPopularPromotions = ItemsList;
            promotionViewModel.AllPromotionsByRegion = promotionsbyregion;

            //to add recommendation based on new promotions
            promotionViewModel.AllNewPromotions = allnewpromotions.ToList();

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
