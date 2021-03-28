using Common.DBTableModels;
using LoginAPI.RepositoryInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginAPI.Controller
{
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ILoginRepository _repository;

        public LoginController(ILogger<LoginController> logger, ILoginRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult Health()
        {
            return Ok("LoginController Healthy at : " + DateTime.Now.ToString());
        }

        [HttpPost]
        [Route("login/insert")]
        public async Task<int> CreateUser()
        {
            UserLogin newUserLogin = new UserLogin
            {
                UserId = "aaaaaaaaa",
                UserLoginId = "PeterTanAbc",
                UserName = "Peter Tan",
                Password = "abc"
            };
            var result = await _repository.CreateUser(newUserLogin);
            return result;
        }

        [HttpPost]
        [Route("login/update")]
        public async Task<int> UpdateUser()
        {
            var toUpdateUserData = await _repository.Login("PeterTanAbc", "abc");
            toUpdateUserData.UserLoginId = "JohnTanAbc";
            toUpdateUserData.UserName = "John Tan";
            //UserLogin newUserLogin = new UserLogin
            //{
            //    UserId = "aaaaaaaaa",
            //    UserLoginId = "JohnTanAbc",
            //    UserName = "John Tan",
            //    Password = "abc"
            //};
            var result = await _repository.UpdateUser(toUpdateUserData);
            return result;
        }

        [HttpGet]
        [Route("login/retrieve")]
        public async Task<UserLogin> Login([FromQuery] string userLoginId, string password)
        {
            var userData = await _repository.Login(userLoginId, password);
            return userData;
        }
    }
}
