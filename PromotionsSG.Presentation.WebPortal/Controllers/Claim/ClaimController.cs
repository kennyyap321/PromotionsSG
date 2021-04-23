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
            return View(); //todo:Claim history
        }

        [HttpGet]
        public async Task<IActionResult> Details(int claimId)
        {
            return View(); //todo:Claim details
        }
        #endregion


        #region Post
        [HttpPost]
        public async Task<IActionResult> Claim(int promotionId)
        {
            string userName = HttpContext.Session.GetString("username");
            CustomerProfiles customerProfile = await _customerProfileService.CustomerProfile(userName);
            int customerProfileId = customerProfile.CustomerProfileId;

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
        #endregion
    }
}
