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
        #endregion


        #region Dependency injection
        public ClaimController(ILogger<ClaimController> logger, IClaimService claimService)
        {
            _logger = logger;
            _claimService = claimService;
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

            var result = await _claimService.ClaimAsync(promotionId, userName);
            if (result != null)
            {
                return new JsonResult(result);
            }

            return new JsonResult(null);
        }
        #endregion
    }
}
