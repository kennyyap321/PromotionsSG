using Common.DBTableModels;
using Microsoft.AspNetCore.Http;
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
    public class ShopProfileController : Controller
    {
        #region Fields
        private readonly ILogger<ShopProfileController> _logger;
        private readonly IShopProfileService _shopProfileService;
        #endregion


        #region Dependency injection
        public ShopProfileController(ILogger<ShopProfileController> logger, IShopProfileService shopProfileService)
        {
            _logger = logger;
            _shopProfileService = shopProfileService;
        }
        #endregion


        #region Get
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int userId = HttpContext.Session.GetInt32("userid").Value;
            string userName = HttpContext.Session.GetString("username");

            ShopProfile result = await _shopProfileService.RetrieveShopProfileByUserIdAsync(userId);
            if (result == null)
            {
                ShopProfile shopProfile = new ShopProfile
                {
                    UserId = userId
                };

                result = await _shopProfileService.InsertAsync(shopProfile);
            }

            return View(new ShopProfileViewModel { ShopProfileDto = result });
        }
        #endregion


        #region Post
        [HttpPost]
        public async Task<IActionResult> Update(ShopProfileViewModel shopProfileViewModel)
        {
            ShopProfile shopProfile = shopProfileViewModel.ShopProfileDto;

            ShopProfile result = await _shopProfileService.UpdateAsync(shopProfile);

            return View("Index", new ShopProfileViewModel { ShopProfileDto = result });
        }
        #endregion
    }
}
