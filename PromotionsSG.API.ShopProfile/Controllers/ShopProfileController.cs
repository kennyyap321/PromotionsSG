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
        #region Fields
        private readonly ILogger<ShopProfileController> _logger;
        private readonly IShopProfileRepository _repository;
        #endregion


        #region Dependency injection
        public ShopProfileController(ILogger<ShopProfileController> logger, IShopProfileRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        #endregion


        #region Health
        public IActionResult Health()
        {
            return Ok($"{nameof(ShopProfileController)} healthy at : {DateTime.Now}");
        }
        #endregion


        #region CRUD
        [HttpGet]
        public async Task<ShopProfile> Retrieve([FromQuery] int shopProfileId)
        {
            var result = await _repository.RetrieveAsync(shopProfileId);

            return result;
        }

        [HttpPost]
        public async Task<ShopProfile> Insert([FromBody] ShopProfile shopProfile)
        {
            var result = await _repository.InsertAsync(shopProfile);

            return result;
        }

        [HttpPost]
        public async Task<ShopProfile> Update([FromBody] ShopProfile shopProfile)
        {
            var result = await _repository.UpdateAsync(shopProfile);

            return result;
        }
        #endregion


        #region Custom
        [HttpGet]
        public async Task<ShopProfile> RetrieveShopProfileByUserId([FromQuery] int userId)
        {
            var result = await _repository.RetrieveShopProfileByUserIdAsync(userId);

            return result;
        }

        [HttpPost]
        public async Task<IEnumerable<ShopProfile>> RetrieveShopProfilesByShopProfileIds([FromBody] IEnumerable<int> shopProfileIds)
        {
            var result = await _repository.RetrieveShopProfilesByShopProfileIdsAsync(shopProfileIds);

            return result;
        }
        #endregion
    }
}
