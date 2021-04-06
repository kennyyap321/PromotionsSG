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

        public async Task<IActionResult> EditCustomerProfile(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = await _customerProfileService.CustomerProfile(id);
            if (entity == null)
            {
                return NotFound();
            }
            return View("EditCustomerProfileSub", entity);
        }

        [HttpPost]
        public async Task<IActionResult> EditCustomerProfileSub([Bind("customerProfileID,customerFullName,customerAddress,customerEmail,customerPhone,customerType,customerGender,customerActive,customerDOB,CreatedBy,CreatedTime,LastUpdatedBy,LastUpdatedTime,VersionNo,IsDeleted")] CustomerProfileViewModel customer)
        {
            using (var httpClient = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(customer.customerProfileID), "CustomerProfileId");
                content.Add(new StringContent(customer.customerFullName), "CustomerFullName");
                content.Add(new StringContent(customer.customerAddress), "CustomerAddress");
                content.Add(new StringContent(customer.customerEmail), "CustomerEmail");
                content.Add(new StringContent(customer.customerPhone), "CustomerPhone");
                content.Add(new StringContent(customer.customerType), "CustomerType");
                content.Add(new StringContent(customer.customerGender), "CustomerGender");
                content.Add(new StringContent(customer.customerActive.ToString()), "CustomerActive");
                content.Add(new StringContent(customer.customerDOB.ToString()), "CustomerDOB");
                content.Add(new StringContent(customer.CreatedBy), "CreatedBy");
                content.Add(new StringContent(customer.CreatedTime.ToString()), "CreatedTime");
                content.Add(new StringContent(customer.LastUpdatedBy), "LastUpdatedBy");
                content.Add(new StringContent(customer.LastUpdatedTime.ToString()), "LastUpdatedTime");
                content.Add(new StringContent(customer.VersionNo.ToString()), "VersionNo");
                content.Add(new StringContent(customer.IsDeleted.ToString()), "IsDeleted");
                using (var response = await httpClient.PutAsync($"http://localhost:9990/customerprofile/update/{customer.customerEmail}", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return View("SuccessCustomerClick");
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
