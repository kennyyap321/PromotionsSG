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

        public CustomerProfileViewController(ILogger<CustomerProfileViewController> logger, ICustomerProfileService customerProfileService)
        {
            _logger = logger;
            _customerProfileService = customerProfileService;
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
            //customer.CustomerProfileId = "987";
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
            email = HttpContext.Session.GetString("username");
            CustomerProfiles customerProfiles = await _customerProfileService.CustomerProfile(email);
            CustomerProfileViewModel customerProfileViewModel = new CustomerProfileViewModel { customerDto = customerProfiles };

            return View("UpdateCustomerProfile", customerProfileViewModel);
        }

        [HttpPost]
        [Route("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomerProfile(CustomerProfileViewModel customerProfileViewModel)
        {
            CustomerProfiles customer = customerProfileViewModel.customerDto;
            string result = await _customerProfileService.UpdateCustomer(customer);

            return View("SuccessCustomerClick", customerProfileViewModel);
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
