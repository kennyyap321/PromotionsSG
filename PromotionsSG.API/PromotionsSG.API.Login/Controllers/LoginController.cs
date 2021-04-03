using Common.DBTableModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PromotionsSG.API.Login.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionsSG.API.Login.Controllers
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

        #region Health
        public IActionResult Health()
        {
            return Ok($"{nameof(LoginController)} healthy at : {DateTime.Now}");
        }
        #endregion


        #region User
        [HttpGet]
        [Route("login/retrieve")]
        public async Task<User> Login([FromQuery] string userName, string password, int userType)
        {
            var result = await _repository.LoginAsync(userName, password, userType);

            return result;
        }

        [HttpPost]
        [Route("login/insert")]
        public async Task<int> CreateUser([FromBody] User userLogin)
        {
            var result = await _repository.CreateUserAsync(userLogin);

            return result;
        }

        [HttpPost]
        [Route("login/update")]
        public async Task<int> UpdateUserAsync([FromBody] User userLogin)
        {
            var result = await _repository.UpdateUserAsync(userLogin);

            return result;
        }
        #endregion
    }
}
