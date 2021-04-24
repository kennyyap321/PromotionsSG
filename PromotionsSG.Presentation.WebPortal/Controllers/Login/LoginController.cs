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
    public class LoginController : Controller
    {
        #region Fields
        private readonly ILogger<LoginController> _logger;
        private readonly ILoginService _loginService;
        #endregion


        #region Dependency injection
        public LoginController(ILogger<LoginController> logger, ILoginService loginService, IShopProfileService shopProfileService, IPromotionService promotionService)
        {
            _logger = logger;
            _loginService = loginService;
        }
        #endregion


        #region Get
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            //HttpContext.Session.Remove("usertype");

            return RedirectToAction("index", "home");
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        #endregion


        #region Post
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = loginViewModel.UserDto;

                User result = await _loginService.LoginAsync(user.UserType, user.UserName, user.Password);
                if (result != null)
                {
                    HttpContext.Session.SetInt32("usertype", result.UserType);
                    HttpContext.Session.SetInt32("userid", result.UserId);
                    HttpContext.Session.SetString("username", result.UserName);

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("Login failed", "Login failed");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(User user)
        {
            User result = await _loginService.UpdateAsync(user);
            if (result != null)
            {
                return new JsonResult(result);
            }

            return new JsonResult(null);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var user = registerViewModel.UserDto;

            User result = await _loginService.RegisterAsync(user);
            if (result != null)
            {
                HttpContext.Session.SetInt32("usertype", result.UserType);
                HttpContext.Session.SetInt32("userid", result.UserId);
                HttpContext.Session.SetString("username", result.UserName);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("Invalid Credentials", "Invalid Credentials");

            return View();
        }
        #endregion
    }
}
