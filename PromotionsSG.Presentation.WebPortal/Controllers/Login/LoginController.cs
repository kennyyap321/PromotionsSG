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
        private readonly ICustomerProfileService _customerProfileService;
        #endregion


        #region Dependency injection
        public LoginController(ILogger<LoginController> logger, ILoginService loginService, IShopProfileService shopProfileService, IPromotionService promotionService, ICustomerProfileService customerProfileService)
        {
            _logger = logger;
            _loginService = loginService;
            _customerProfileService = customerProfileService;
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

                    if (result.UserType == 1)
                    {
                        return Redirect("/PromotionByShopId");
                    }
                    else
                    {
                        return Redirect("/PromotionByRecommendation");
                    }
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
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel, CustomerProfiles customer)
        {
            var user = registerViewModel.UserDto;

            if (registerViewModel.UserDto.UserType == 2 && registerViewModel.UserDto.UserName != null && registerViewModel.UserDto.Password != null)
            {
                customer.CustomerEmail = user.UserName;
                customer.CustomerFullName = "not set";
                customer.CustomerPhone = "not set";
                customer.CustomerGender = "Not set";
                customer.CustomerAddress = "not set";
                customer.CustomerDOB = DateTime.Today;
                customer.CustomerType = "Normal";
                customer.CustomerActive = true;
                customer.CreatedBy = "System";
                customer.CreatedTime = DateTime.Now;
                customer.LastUpdatedBy = "System";
                customer.LastUpdatedTime = DateTime.Now;
                customer.VersionNo = 1;
                customer.IsDeleted = false;
                customer.PostalCode = "not set";
                customer.Region = "not set";
                string insertCustomerData = await _customerProfileService.CreateCustomer(customer);
            }

            User result = await _loginService.RegisterAsync(user);
            if (result != null)
            {
                HttpContext.Session.SetInt32("usertype", result.UserType);
                HttpContext.Session.SetInt32("userid", result.UserId);
                HttpContext.Session.SetString("username", result.UserName);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("Register Failed", "Register Failed");

            return View();
        }
        #endregion
    }
}
