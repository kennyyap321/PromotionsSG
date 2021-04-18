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
        #region Fields
        private readonly ILogger<LoginController> _logger;
        private readonly ILoginRepository _repository;
        #endregion


        #region Dependency injection
        public LoginController(ILogger<LoginController> logger, ILoginRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        #endregion


        #region Health
        public IActionResult Health()
        {
            return Ok($"{nameof(LoginController)} healthy at : {DateTime.Now}");
        }
        #endregion


        #region CRUD
        [HttpGet]
        public async Task<User> Retrieve(int userId)
        {
            var result = await _repository.RetrieveAsync(userId);

            return result;
        }

        [HttpPost]
        public async Task<User> Insert([FromBody] User user)
        {
            var result = await _repository.InsertAsync(user);

            return result;
        }

        [HttpPost]
        public async Task<User> Update([FromBody] User user)
        {
            var result = await _repository.UpdateAsync(user);

            return result;
        }
        #endregion


        #region Custom
        [HttpPost]
        [Route("login/login")]
        public async Task<User> Login([FromBody] User user)
        {
            var result = await _repository.LoginAsync(user);

            return result;
        }
        #endregion
    }
}
