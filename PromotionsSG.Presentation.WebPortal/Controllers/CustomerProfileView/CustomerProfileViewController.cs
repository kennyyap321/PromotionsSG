using Common.DBTableModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PromotionsSG.Presentation.WebPortal.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using Common.AppSettings;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace PromotionsSG.Presentation.WebPortal.Controllers
{
    public class CustomerProfileViewController : Controller
    {
        private readonly ILogger<CustomerProfileViewController> _logger;
        private readonly ICustomerProfileService _customerProfileService;
        private readonly IRecommendationService _recommendationService;

        public CustomerProfileViewController(ILogger<CustomerProfileViewController> logger, ICustomerProfileService customerProfileService, IRecommendationService recommendationService)
        {
            _logger = logger;
            _customerProfileService = customerProfileService;
            _recommendationService = recommendationService;
        }

        [Route("InsertCustomerProfile")]
        public IActionResult InsertCustomerProfile()
        {
            return View();
        }

        [HttpPost]
        [Route("InsertCustomerProfile")]
        public async Task<IActionResult> InsertCustomerProfile(CustomerProfileViewModel customerProfileViewModel)
        {
            CustomerProfiles customer = customerProfileViewModel.customerDto;
            customer.CustomerType = "Normal";
            customer.CustomerActive = true;
            customer.CreatedBy = "System";
            customer.CreatedTime = DateTime.Now;
            customer.LastUpdatedBy = "System";
            customer.LastUpdatedTime = DateTime.Now;
            customer.VersionNo = 1;
            customer.IsDeleted = false;
            string result = await _customerProfileService.CreateCustomer(customer);

            return View("SuccessCustomerClick", customerProfileViewModel);
        }

        public async Task<IActionResult> SuccessfulCustomerClick(CustomerProfileViewModel customerProfileViewModel)
        {
            CustomerProfiles resultSet = await _customerProfileService.CustomerProfile(customerProfileViewModel.customerEmail);
            return View("SuccessCustomerClick", resultSet);
        }

        public async Task<IActionResult> ViewCustomerProfile()
        {
            var username = HttpContext.Session.GetString("username");
            return View(await _customerProfileService.CustomerProfile(username));
        }


        [HttpGet]
        [Route("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomerProfile([FromQuery] string email)
        {
            var message = TempData["message"];

            email = HttpContext.Session.GetString("username");
            CustomerProfiles customerProfiles = await _customerProfileService.CustomerProfile(email);
            CustomerProfileViewModel customerProfileViewModel = new CustomerProfileViewModel { customerDto = customerProfiles, Message = message?.ToString() ?? string.Empty };

            return View("UpdateCustomerProfile", customerProfileViewModel);
        }

        [HttpPost]
        [Route("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomerProfile(CustomerProfileViewModel customerProfileViewModel)
        {
            CustomerProfiles customer = customerProfileViewModel.customerDto;

            string stringToCheck = customer.PostalCode;

            List<CodeTable> centralRegion = await _recommendationService.GetCentralRegionByPostalCode();
            List<CodeTable> eastRegion = await _recommendationService.GetEastRegionByPostalCode();
            List<CodeTable> northRegion = await _recommendationService.GetNorthRegionByPostalCode();
            List<CodeTable> NERegion = await _recommendationService.GetNERegionByPostalCode();
            List<CodeTable> westRegion = await _recommendationService.GetWestRegionByPostalCode();

            foreach (var x in centralRegion)
            {
                if (stringToCheck.Substring(0,2) == x.Code)
                //if(stringToCheck.Contains(x.Code))
                {
                    customer.Region = "Central";
                }
            }

            foreach (var x in eastRegion)
            {
                if (stringToCheck.Substring(0, 2) == x.Code)
                {
                    customer.Region = "East";
                }
            }

            foreach (var x in northRegion)
            {
                if (stringToCheck.Substring(0, 2) == x.Code)
                {
                    customer.Region = "North";
                }
            }

            foreach (var x in NERegion)
            {
                if (stringToCheck.Substring(0, 2) == x.Code)
                {
                    customer.Region = "North-East";
                }
            }

            foreach (var x in westRegion)
            {
                if (stringToCheck.Substring(0, 2) == x.Code)
                {
                    customer.Region = "West";
                }
            }

            await _customerProfileService.UpdateCustomer(customer);

            var username = HttpContext.Session.GetString("username");
            TempData["message"] = "Updated Successfully";
            var result = await _customerProfileService.CustomerProfile(username);

            return RedirectToAction("UpdateCustomerProfile", new { customerEmail = result.CustomerEmail });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
