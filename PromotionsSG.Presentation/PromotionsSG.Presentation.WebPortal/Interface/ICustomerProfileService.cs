using Common.DBTableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionsSG.Presentation.WebPortal
{
    public interface ICustomerProfileService
    {
        Task<CustomerProfiles> CustomerProfile(string customerEmail);
        Task<List<CustomerProfiles>> GetCustomerProfiles();
        Task<string> CreateCustomer(CustomerProfiles customer);
        Task<string> UpdateCustomer(CustomerProfiles customer);

    }
}
