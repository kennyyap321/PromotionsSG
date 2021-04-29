using CommonDB = Common.DBTableModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PromotionsSG.API.PromotionAPI.Repository;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

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
        public async Task<List<CommonDB.Promotion>> RetrievePromotionByShopId([FromQuery] int shopId)
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

        [HttpGet]
        [Route("promotion/retrievebyregion")]
        public async Task<ActionResult<IEnumerable<CommonDB.Promotion>>> RetrievePromotionByRegion([FromQuery] string region)
        {
            try
            {
                var result = await _repository.RetrievePromotionByRegionAsync(region);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
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
        public async Task<CommonDB.Promotion> UpdatePromotion([FromBody] CommonDB.Promotion promotion)
        {
            var result = await _repository.UpdatePromotionAsync(promotion);

            return result;
        }

        [HttpPost]
        [Route("promotion/DeletePromotion")]
        public async Task<CommonDB.Promotion> DeletePromotion([FromBody] CommonDB.Promotion promotion)
        {
            var result = await _repository.DeletePromotionAsync(promotion);

            return result;
        }

        [HttpGet]
        [Route("promotion/retrieveAll")]
        public async Task<ActionResult> AllPromotions()
        {
            try
            {
                return Ok(await _repository.GetAllPromotions());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet]
        [Route("promotion/SearchPromotion")]
        public async Task<ActionResult<IEnumerable<CommonDB.Promotion>>> Search([FromQuery] string searchTerm)
        {
            try
            {
                var result = await _repository.Search(searchTerm);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<IEnumerable<CommonDB.Promotion>> RetrievePromotionsByPromotionIds([FromBody] IEnumerable<int> promotionIds)
        {
            var result = await _repository.RetrievePromotionsByPromotionIdsAsync(promotionIds);

            return result;
        }

        [HttpGet]
        public async Task<IEnumerable<CommonDB.Promotion>> RetrieveNewestPromotions()
        {
            var result = await _repository.RetrieveNewestPromotionsAsync();

            return result;
        }
        #endregion
    }
}
