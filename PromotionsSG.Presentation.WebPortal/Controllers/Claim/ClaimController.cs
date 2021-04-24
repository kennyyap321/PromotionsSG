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
    public class ClaimController : Controller
    {
        #region Fields
        private readonly ILogger<ClaimController> _logger;
        private readonly IClaimService _claimService;
        private readonly ICustomerProfileService _customerProfileService;
        #endregion


        #region Dependency injection
        public ClaimController(ILogger<ClaimController> logger, IClaimService claimService, ICustomerProfileService customerProfileService)
        {
            _logger = logger;
            _claimService = claimService;
            _customerProfileService = customerProfileService;
        }
        #endregion


        #region Get
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userName = HttpContext.Session.GetString("username");
            int customerProfileId = (await _customerProfileService.CustomerProfile(userName)).CustomerProfileId;

            IEnumerable<ClaimWithPromotionAndShopInfo> cwpasis = await _claimService.RetrieveClaimsWithPromotionAndShopInfoByCustomerProfileIdAsync(customerProfileId);
            ClaimViewModel claimViewModel = new ClaimViewModel { ClaimExtraInfoListDto = cwpasis };

            return View(claimViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int claimId)
        {
            ClaimWithPromotionAndShopInfo cwpasi = await _claimService.RetrieveClaimWithPromotionAndShopInfoByClaimIdAsync(claimId);
            ClaimViewModel claimViewModel = new ClaimViewModel { ClaimExtraInfo = cwpasi };

            return View(claimViewModel);
        }
        #endregion


        #region Post
        [HttpPost]
        public async Task<IActionResult> Claim(int promotionId)
        {
            string userName = HttpContext.Session.GetString("username");
            int customerProfileId = (await _customerProfileService.CustomerProfile(userName)).CustomerProfileId;

            Claim claim = new Claim
            {
                PromotionId = promotionId,
                CustomerProfileId = customerProfileId
            };

            var result = await _claimService.ClaimAsync(claim);
            if (result != null)
            {
                return new JsonResult(result);
            }

            return new JsonResult(null);
        }

        [HttpPost]
        public async Task<IActionResult> Use(int claimId)
        {
            Claim claim = await _claimService.RetrieveAsync(claimId);

            claim.Consumed = true;
            claim.ConsumeDate = DateTime.Now;

            await _claimService.UpdateAsync(claim);

            return RedirectToAction("Details", "Claim", new { claimId });
        }
        #endregion
    }
}
