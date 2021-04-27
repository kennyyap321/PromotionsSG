using Common.DBTableModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PromotionsSG.Presentation.WebPortal.Models;
using PromotionsSG.Presentation.WebPortal.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PromotionsSG.Presentation.WebPortal.Controllers
{
    public class PromotionViewController : Controller
    {
        private readonly ILogger<PromotionViewController> _logger;
        private readonly IPromotionService _promotionService;
        private readonly IShopProfileService _shopProfileService;

        public PromotionViewController(ILogger<PromotionViewController> logger, IPromotionService promotionService, IShopProfileService shopProfileService)
        {
            _logger = logger;
            _promotionService = promotionService;
            _shopProfileService = shopProfileService;
        }

        #region Promotion
        [HttpGet]
        [Route("/PromotionByShopId")]
        public async Task<IActionResult> PromotionByShopId()
        {
            var userId = HttpContext.Session.GetInt32("userid").Value;
            var shopProfileId = (await _shopProfileService.RetrieveShopProfileByUserIdAsync(userId))?.ShopProfileId;
            var promotion = await _promotionService.RetrievePromotionByShopIdAsync((int)shopProfileId);
            var promotionViewModel = new PromotionViewModel { Promotions = promotion };
            return View("Index", promotionViewModel);
        }

        [HttpGet]
        [Route("/AddPromotion")]
        public async Task<IActionResult> Register()
        {
            var message = TempData["message"];
            PromotionViewModel promotionViewModel = new PromotionViewModel { Message = message?.ToString() ?? string.Empty };
            return View("AddPromotion", promotionViewModel);
        }

        [HttpGet]
        [Route("/promotion")]
        public async Task<IActionResult> Promotion([FromQuery] int promotionId)
        {
            var message = TempData["message"];
            Promotion promotion = await _promotionService.RetrievePromotionAsync(promotionId);
            PromotionViewModel promotionViewModel = new PromotionViewModel { PromotionDto = promotion, Message = message?.ToString() ?? string.Empty };

            return View("UpdatePromotion", promotionViewModel);
        }

        [HttpPost]
        [Route("/CreatePromotion")]
        public async Task<IActionResult> CreatePromotion(PromotionViewModel promotionViewModel)
        {
            Promotion promotion = promotionViewModel.PromotionDto;
            var userId = HttpContext.Session.GetInt32("userid").Value;
            promotion.ShopProfileId = (int)((await _shopProfileService.RetrieveShopProfileByUserIdAsync(userId))?.ShopProfileId);
            promotion.Type = 2;
            promotion.IsActive = true;
            if (promotion.StartDate == DateTime.MinValue)
            {
                promotion.StartDate = DateTime.Now;
            }
            if (promotion.EndDate == DateTime.MinValue)
            {
                promotion.EndDate = DateTime.Now.AddYears(1);
            }
            await _promotionService.CreatePromotionAsync(promotion);
            TempData["message"] = "Create Successfully";
            return RedirectToAction("Register");
        }

        [HttpPost]
        [Route("/UpdatePromotion")]
        public async Task<IActionResult> UpdatePromotion(PromotionViewModel promotionViewModel)
        {
            Promotion promotion = promotionViewModel.PromotionDto;
            var result = await _promotionService.UpdatePromotionAsync(promotion);
            TempData["message"] = "Update Successfully";
            return RedirectToAction("Promotion", new { promotionId = result.PromotionId });
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
