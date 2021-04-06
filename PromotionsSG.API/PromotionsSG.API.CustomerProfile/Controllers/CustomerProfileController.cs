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
        public async Task<int> CreateCustomerProfile()
        {
            CustomerProfiles newCustomer = new CustomerProfiles
            {
                CustomerProfileId = "123",
                CustomerEmail = "john@mail.com",
                CustomerFullName = "John Holmes"
            };
            var result = await _repository.CreateCustomerProfile(newCustomer);
            return result;
        }

        [HttpPut]
        [Route("customerprofile/update/{id}")]
        public async Task<ActionResult<CustomerProfiles>> UpdateCustomerProfile(string id, [FromForm] CustomerProfiles customerProfiles) //[FromBody]
        {
            try
            {
                if (id != customerProfiles.CustomerEmail)
                    return BadRequest("Customer ID mismatch");

                var customerToUpdate = await _repository.Customer(id);

                if (customerProfiles == null)
                    return NotFound($"Customer with email = {id} not found");

                return await _repository.UpdateCustomerProfile(customerProfiles);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
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
