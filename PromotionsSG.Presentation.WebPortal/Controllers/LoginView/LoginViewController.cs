using Common.DBTableModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PromotionsSG.Presentation.WebPortal.Models;
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

        public LoginViewController(ILogger<LoginViewController> logger, ILoginService loginService)
        {
            _logger = logger;
            _loginService = loginService;
        }

        public async Task<IActionResult> UserLogin(LoginViewModel loginViewModel)
        {
            UserLogin resultSet = await _loginService.Login(loginViewModel.userName, loginViewModel.password);
            return View("SuccessLogin", resultSet);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
