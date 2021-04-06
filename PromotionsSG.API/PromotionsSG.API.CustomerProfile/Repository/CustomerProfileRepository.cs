using Common.DBTableModels;
using PromotionsSG.API.CustomerProfile.RepositoryInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionsSG.API.CustomerProfile.Repository
{
    public class CustomerProfileRepository : ICustomerProfileRepository
    {
        private readonly MyDBContext _context;

        public CustomerProfileRepository(MyDBContext context)
        {
            _context = context;
        }
        public async Task<int> CreateCustomerProfile(CustomerProfiles customerProfile)
        {
            _context.Add(customerProfile);
            await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<CustomerProfiles> UpdateCustomerProfile(CustomerProfiles customerProfile)
        {
            var result = 
            await _context.CustomerProfile
            .FirstOrDefaultAsync(e => e.CustomerEmail == customerProfile.CustomerEmail);

            if (result != null)
            {
                result.CustomerProfileId = customerProfile.CustomerProfileId;
                result.CustomerFullName = customerProfile.CustomerFullName;
                result.CustomerAddress = customerProfile.CustomerAddress;
                result.CustomerEmail = customerProfile.CustomerEmail;
                result.CustomerPhone = customerProfile.CustomerPhone;
                result.CustomerType = customerProfile.CustomerType;
                result.CustomerGender = customerProfile.CustomerGender;
                result.CustomerActive = customerProfile.CustomerActive;
                result.CustomerDOB = customerProfile.CustomerDOB;
                result.CreatedBy = customerProfile.CreatedBy;
                result.CreatedTime = customerProfile.CreatedTime;
                result.LastUpdatedBy = customerProfile.LastUpdatedBy;
                result.LastUpdatedTime = customerProfile.LastUpdatedTime;
                result.VersionNo = customerProfile.VersionNo;
                result.IsDeleted = customerProfile.IsDeleted;

                await _context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<CustomerProfiles> Customer(string customerEmail)
        {
            //use .where for multiple records
            var customerProfileData = await _context.CustomerProfile.FirstOrDefaultAsync(x => x.CustomerEmail == customerEmail);
            return customerProfileData;
        }
        //var customerProfileData = await _context.CustomerProfile.Where(x => x.CustomerActive == true).ToListAsync();
        public async Task<IEnumerable<CustomerProfiles>> GetCustomerProfiles()
        {
            var customerProfileData = await _context.CustomerProfile.Where(x => x.CustomerActive == true).ToListAsync();
            return customerProfileData;
        }
    }
}
