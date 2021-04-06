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
    public class LoginViewController : Controller
    {
        private readonly ILogger<LoginViewController> _logger;
        private readonly ILoginService _loginService;

        public LoginViewController(ILogger<LoginViewController> logger, ILoginService loginService, IShopProfileService shopProfileService, IPromotionService promotionService)
        {
            _logger = logger;
            _loginService = loginService;
        }

        #region Login
        [HttpGet]
        [Route("/login")]
        public async Task<IActionResult> Login()
        {
            LoginViewModel loginViewModel = new LoginViewModel { userDto = new User { UserType = 1 } };

            return View("Index", loginViewModel);
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var user = loginViewModel.userDto;

            var result = await _loginService.LoginAsync(user.UserName, user.Password, user.UserType);

            if (result == null)
                return View("UnsuccessLogin");

            HttpContext.Session.SetString("username", user.UserName);

            return View("SuccessLogin", new LoginViewModel { userDto = result });
        }
        #endregion


        #region Register
        [HttpGet]
        [Route("/register")]
        public async Task<IActionResult> Register()
        {
            LoginViewModel loginViewModel = new LoginViewModel { userDto = new User { UserType = 1 } };

            return View("Register", loginViewModel);
        }

        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> Register(LoginViewModel loginViewModel)
        {
            var user = loginViewModel.userDto;
            var result = await _loginService.RegisterUserAsync(user);

            if (result == -1)
                return View("UnsuccessLogin");

            var result2 = await _loginService.LoginAsync(user.UserName, user.Password, user.UserType);

            if (result2 == null)
                return View("UnsuccessLogin");

            return View("SuccessLogin", new LoginViewModel { userDto = result2 });
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
