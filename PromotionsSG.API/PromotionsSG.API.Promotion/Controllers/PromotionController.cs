using CommonDB = Common.DBTableModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PromotionsSG.API.PromotionAPI.Repository;
using System;
using System.Threading.Tasks;

namespace PromotionsSG.API.PromotionAPI.Controllers
{
    public class PromotionController : ControllerBase
    {
        private readonly ILogger<PromotionController> _logger;
        private readonly IPromotionRepository _repository;

        public PromotionController(ILogger<PromotionController> logger, IPromotionRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        #region Health
        public IActionResult Health()
        {
            return Ok($"{nameof(PromotionController)} healthy at : {DateTime.Now}");
        }
        #endregion


        #region Promotion
        [HttpGet]
        [Route("promotion/PromotionByShopId")]
        public async Task<CommonDB.Promotion> RetrievePromotionByShopId([FromQuery] int shopId)
        {
            var result = await _repository.RetrievePromotionByShopIdAsync(shopId);

            return result;
        }

        [HttpGet]
        [Route("promotion/retrieve")]
        public async Task<CommonDB.Promotion> RetrievePromotionById([FromQuery] int promotionId)
        {
            var result = await _repository.RetrievePromotionByIdAsync(promotionId);

            return result;
        }

        [HttpPost]
        [Route("promotion/CreatePromotion")]
        public async Task<int> CreatePromotion([FromBody] CommonDB.Promotion promotion)
        {
            var result = await _repository.CreatePromotionAsync(promotion);

            return result;
        }

        [HttpPost]
        [Route("promotion/UpdatePromotion")]
        public async Task<int> UpdatePromotionLogin([FromBody] CommonDB.Promotion promotion)
        {
            var result = await _repository.UpdatePromotionAsync(promotion);

            return result;
        }
        #endregion
    }
}
