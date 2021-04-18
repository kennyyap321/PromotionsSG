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
    public class AccountViewComponent : ViewComponent
    {
        #region Fields
        private readonly ILoginService _loginService;
        #endregion


        #region Dependency injection
        public AccountViewComponent(ILoginService loginService)
        {
            _loginService = loginService;
        }
        #endregion


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = HttpContext.Session.GetInt32("userid");
            if (userId != null)
            {
                User result = await _loginService.RetrieveAsync(userId.Value);

                return View(new AccountViewModel { UserDto = result });
            }

            return View();
        }
    }
}
