using Common.DBTableModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PromotionsSG.Presentation.WebPortal.Models;
using PromotionsSG.Presentation.WebPortal.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionsSG.Presentation.WebPortal.Controllers
{
    public class ShopProfileViewController : Controller
    {
        private readonly ILogger<ShopProfileViewController> _logger;
        private readonly IShopProfileService _shopProfileService;

        public ShopProfileViewController(ILogger<ShopProfileViewController> logger, IShopProfileService shopProfileService)
        {
            _logger = logger;
            _shopProfileService = shopProfileService;
        }

        #region ShopProfile
        [HttpGet]
        [Route("/shopProfile")]
        public async Task<IActionResult> ShopProfile([FromQuery] int shopProfileId)
        {
            ShopProfile shopProfile = await _shopProfileService.RetrieveShopProfileAsync(shopProfileId);
            ShopProfileViewModel shopProfileViewModel = new ShopProfileViewModel { shopProfileDto = shopProfile };

            return View("Index", shopProfileViewModel);
        }

        [HttpPost]
        [Route("/shopProfile")]
        public async Task<IActionResult> UpdateShopProfile(ShopProfileViewModel shopProfileViewModel)
        {
            ShopProfile shopProfile = shopProfileViewModel.shopProfileDto;
            int result = await _shopProfileService.UpdateShopProfileAsync(shopProfile);

            return View("Index", shopProfileViewModel);
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
