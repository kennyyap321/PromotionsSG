using Common.DBTableModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PromotionsSG.API.CustomerProfile.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionsSG.API.CustomerProfile.Controllers
{
    public class CustomerProfileController : ControllerBase
    {
        private readonly ILogger<CustomerProfileController> _logger;
        private readonly ICustomerProfileRepository _repository;

        public CustomerProfileController(ILogger<CustomerProfileController> logger, ICustomerProfileRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult Health()
        {
            return Ok("CustomerProfileController Healthy at : " + DateTime.Now.ToString());
        }

        [HttpPost]
        [Route("customerprofile/insert")]
        public async Task<string> CreateCustomerProfile([FromBody] CustomerProfiles customerProfiles)
        {
            var result = await _repository.CreateCustomerProfile(customerProfiles);

            return result;
        }

        [HttpPost]
        [Route("customerprofile/updateCustomer")]
        public async Task<string> UpdateCustomer([FromBody] CustomerProfiles customerProfiles)
        {
            var result = await _repository.UpdateCustomer(customerProfiles);

            return result;
        }

        [HttpGet]
        [Route("customerprofile/retrieve")]
        public async Task<CustomerProfiles> Customer([FromQuery] string customerEmail)
        {
            var customerData = await _repository.Customer(customerEmail);
            return customerData;
        }

        [HttpGet]
        [Route("customerprofile/retrieveallcustomers")]
        public async Task<ActionResult> AllCustomers()
        {
            try
            {
                return Ok(await _repository.GetCustomerProfiles());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}
