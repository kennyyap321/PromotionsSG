using Common.DBTableModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PromotionsSG.API.Recommendation.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionsSG.API.Recommendation.Controllers
{
    public class RecommendationController : ControllerBase
    {
        private readonly ILogger<RecommendationController> _logger;
        private readonly IRecommendationRepository _repository;

        public RecommendationController(ILogger<RecommendationController> logger, IRecommendationRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult Health()
        {
            return Ok("RecommendationController Healthy at : " + DateTime.Now.ToString());
        }

        [HttpPost]
        [Route("recommendation/insert")]
        public async Task<int> CreateRecommendation([FromBody] Recommendations recommendations)
        {
            var result = await _repository.CreateRecommendation(recommendations);

            return result;
        }

        [HttpPost]
        [Route("recommendation/updateRecommendation")]
        public async Task<int> UpdateRecommendation([FromBody] Recommendations recommendations)
        {
            var result = await _repository.UpdateRecommendation(recommendations);

            return result;
        }

        [HttpGet]
        [Route("recommendation/retrieve")]
        public async Task<Recommendations> Recommendation([FromQuery] int recommendationId)
        {
            var recommendationData = await _repository.Recommendation(recommendationId);
            return recommendationData;
        }

        [HttpGet]
        [Route("recommendation/retrieveallrecommendations")]
        public async Task<ActionResult> AllRecommendations()
        {
            try
            {
                return Ok(await _repository.GetRecommendations());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        #region get region by postal code

        [HttpGet]
        [Route("recommendation/retrieveCentralPostalCode")]
        public async Task<ActionResult> GetCentralRegionPostalCode()
        {
            try
            {
                return Ok(await _repository.GetCentralRegion());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet]
        [Route("recommendation/retrieveEastPostalCode")]
        public async Task<ActionResult> GetEastRegionPostalCode()
        {
            try
            {
                return Ok(await _repository.GetEastRegion());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet]
        [Route("recommendation/retrieveNorthPostalCode")]
        public async Task<ActionResult> GetNorthRegionPostalCode()
        {
            try
            {
                return Ok(await _repository.GetNorthRegion());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet]
        [Route("recommendation/retrieveNEPostalCode")]
        public async Task<ActionResult> GetNERegionPostalCode()
        {
            try
            {
                return Ok(await _repository.GetNERegion());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet]
        [Route("recommendation/retrieveWestPostalCode")]
        public async Task<ActionResult> GetWestRegionPostalCode()
        {
            try
            {
                return Ok(await _repository.GetWestRegion());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        #endregion
    }
}
