using Common.DBTableModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PromotionsSG.API.ShopProfileAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionsSG.API.ShopProfileAPI.Controllers
{
    public class ShopProfileController : ControllerBase
    {
        private readonly ILogger<ShopProfileController> _logger;
        private readonly IShopProfileRepository _repository;

        public ShopProfileController(ILogger<ShopProfileController> logger, IShopProfileRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        #region Health
        public IActionResult Health()
        {
            return Ok($"{nameof(ShopProfileController)} healthy at : {DateTime.Now}");
        }
        #endregion


        #region ShopProfile
        [HttpGet]
        [Route("shopProfile/retrieve")]
        public async Task<ShopProfile> RetrieveShopProfileById([FromQuery] int shopProfileId)
        {
            var result = await _repository.RetrieveShopProfileByIdAsync(shopProfileId);

            return result;
        }

        [HttpPost]
        [Route("shopProfile/insert")]
        public async Task<int> CreateShopProfile([FromBody] ShopProfile shopProfile)
        {
            var result = await _repository.CreateShopProfileAsync(shopProfile);

            return result;
        }

        [HttpPost]
        [Route("shopProfile/update")]
        public async Task<int> UpdateShopProfileLogin([FromBody] ShopProfile shopProfile)
        {
            var result = await _repository.UpdateShopProfileAsync(shopProfile);

            return result;
        }
        #endregion
    }
}
