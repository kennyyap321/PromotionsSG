using Common.DBTableModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromotionsSG.Presentation.WebPortal.Models;
using PromotionsSG.Presentation.WebPortal.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionsSG.Presentation.WebPortal.ViewComponents
{
    public class ClaimButtonViewComponent : ViewComponent
    {
        #region Fields
        private readonly ILoginService _loginService;
        #endregion


        #region Dependency injection
        public ClaimButtonViewComponent()
        {
        }
        #endregion


        #region Logic
        public async Task<IViewComponentResult> InvokeAsync(int promotionId)
        {
            //Initialize ClaimButtonViewModel
            ClaimButtonViewModel claimButtonViewModel = new ClaimButtonViewModel { };

            //Get userId
            var userId = HttpContext.Session.GetInt32("userid");
            if (userId != null)
                claimButtonViewModel.UserId = userId;

            //Get promotionId passed in
            claimButtonViewModel.PromotionId = promotionId;

            //If view component already invoked before on the same page, dont load the script section again
            bool alreadyInvokedOnSamePage = HttpContext.Items.ContainsKey("ClaimButton");
            if (!alreadyInvokedOnSamePage)
            {
                claimButtonViewModel.InitializeSection = true;
                HttpContext.Items["ClaimButton"] = true;
            }

            //Return view
            return View(claimButtonViewModel);
        }
        #endregion
    }
}
