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

namespace PromotionsSG.Presentation.WebPortal.Controllers
{
    public class PromotionViewController : Controller
    {
        private readonly ILogger<PromotionViewController> _logger;
        private readonly IPromotionService _promotionService;

        public PromotionViewController(ILogger<PromotionViewController> logger, IPromotionService promotionService)
        {
            _logger = logger;
            _promotionService = promotionService;
        }

        #region Promotion
        [HttpGet]
        [Route("/PromotionByShopId")]
        public async Task<IActionResult> PromotionByShopId([FromQuery] int shopId)
        {
            Promotion promotion = await _promotionService.RetrievePromotionByShopIdAsync(shopId);
            PromotionViewModel promotionViewModel = new PromotionViewModel { PromotionDto = promotion };

            return View("Index", promotionViewModel);
        }

        [HttpGet]
        [Route("/AddPromotion")]
        public async Task<IActionResult> Register()
        {
            PromotionViewModel promotionViewModel = new PromotionViewModel();

            return View("AddPromotion", promotionViewModel);
        }

        [HttpGet]
        [Route("/promotion")]
        public async Task<IActionResult> Promotion([FromQuery] int promotionId)
        {
            Promotion promotion = await _promotionService.RetrievePromotionAsync(promotionId);
            PromotionViewModel promotionViewModel = new PromotionViewModel { PromotionDto = promotion };

            return View("Index", promotionViewModel);
        }

        [HttpPost]
        [Route("/CreatePromotion")]
        public async Task<IActionResult> CreatePromotion(PromotionViewModel promotionViewModel)
        {
            Promotion promotion = promotionViewModel.PromotionDto;
            promotion.ShopProfileId = 1;
            promotion.Type = 1;
            promotion.IsActive = true;
            promotion.StartDate = DateTime.Now;
            promotion.EndDate = DateTime.Now;
            int result = await _promotionService.CreatePromotionAsync(promotion);

            return View("Index", promotionViewModel);
        }

        [HttpPost]
        [Route("/UpdatePromotion")]
        public async Task<IActionResult> UpdatePromotion(PromotionViewModel promotionViewModel)
        {
            Promotion promotion = promotionViewModel.PromotionDto;
            int result = await _promotionService.UpdatePromotionAsync(promotion);

            return View("Index", promotionViewModel);
        }

        [HttpGet]
        [Route("GetPromotion")]
        public async Task<IActionResult> GetPromotion(string id)
        {
            Promotion promotion = await _promotionService.RetrievePromotionAsync(Convert.ToInt32(id));
            PromotionViewModel promotionViewModel = new PromotionViewModel { PromotionDto = promotion };

            return View("Index", promotionViewModel);
        }

        [Route("SearchPromotions")]
        public async Task<IActionResult> CustomerViewPromotions([Bind("SearchTerm")] PromotionViewModel promotionModel)
        {
            if (promotionModel.SearchTerm != null)
            {
                List<Promotion> promotion = await _promotionService.Search(promotionModel.SearchTerm);

                if (promotion == null)
                {
                    ViewBag.SearchResult = "No results found";
                    return View();
                }

                PromotionViewModel promotionViewModel = new PromotionViewModel { Promotions = promotion };
                return View(promotionViewModel);
            }

            List<Promotion> promotions = await _promotionService.GetAllPromotions();
            if (promotions == null || promotions.Count < 1)
            {
                //to return empty if no results returned
                ViewBag.SearchResult = "No promotions available";
                return View();
            }

            // To show all promotions upon page load for the 1st time
            PromotionViewModel promotionsViewModel = new PromotionViewModel { Promotions = promotions };

            return View(promotionsViewModel);

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
