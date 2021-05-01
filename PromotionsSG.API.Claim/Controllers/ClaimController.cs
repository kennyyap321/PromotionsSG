using Common.DBTableModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PromotionsSG.API.ClaimAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionsSG.API.ClaimAPI.Controllers
{
    public class ClaimController : ControllerBase
    {
        #region Fields
        private readonly ILogger<ClaimController> _logger;
        private readonly IClaimRepository _repository;
        #endregion


        #region Dependency injection
        public ClaimController(ILogger<ClaimController> logger, IClaimRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        #endregion


        #region Health
        public IActionResult Health()
        {
            return Ok($"{nameof(ClaimController)} healthy at : {DateTime.Now}");
        }
        #endregion


        #region CRUD
        [HttpGet]
        public async Task<Claim> Retrieve([FromQuery] int claimId)
        {
            var result = await _repository.RetrieveAsync(claimId);

            return result;
        }

        [HttpGet]
        public async Task<IEnumerable<Claim>> RetrieveAll()
        {
            var result = await _repository.RetrieveAllAsync();

            return result;
        }

        [HttpPost]
        public async Task<Claim> Insert([FromBody] Claim claim)
        {
            var result = await _repository.InsertAsync(claim);

            return result;
        }

        [HttpPost]
        public async Task<Claim> Update([FromBody] Claim claim)
        {
            var result = await _repository.UpdateAsync(claim);

            return result;
        }
        #endregion


        #region Custom
        [HttpPost]
        public async Task<Claim> Claim([FromBody] Claim claim)
        {
            _logger.LogInformation("Claim API Object: " + JsonConvert.SerializeObject(claim));
            var result = await _repository.ClaimAsync(claim);

            return result;
        }

        [HttpGet]
        public async Task<IEnumerable<Claim>> RetrieveByCustomerProfileId(int customerProfileId)
        {
            var result = await _repository.RetrieveByCustomerProfileIdAsync(customerProfileId);

            return result;
        }

        [HttpGet]
        [Route("claim/retrievepromotion")]
        public async Task<ActionResult> RetrieveByPromotion()
        {
            try
            {
                return Ok(await _repository.RetrieveClaimByPromotionAsync());
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
