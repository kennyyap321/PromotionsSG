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
        public async Task<string> CreateCustomerProfile(CustomerProfiles customerProfile)
        {
            _context.Add(customerProfile);
            var result = await _context.SaveChangesAsync();

            var createdCustomerEmail = (await _context.CustomerProfile.FirstAsync(s => s.CustomerEmail == customerProfile.CustomerEmail)).CustomerEmail;
            return createdCustomerEmail;
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

        public async Task<string> UpdateCustomer(CustomerProfiles customerProfiles)
        {
            _context.CustomerProfile.Update(customerProfiles);
            var result = await _context.SaveChangesAsync();

            var updatedCustomerEmail = customerProfiles.CustomerEmail;

            return updatedCustomerEmail;
        }
    }
}
