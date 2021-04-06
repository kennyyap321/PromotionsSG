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
        //Task EditCustomerProfile(CustomerProfiles customerProfiles);
        //Task<CustomerProfiles> AllCustomerProfiles(string customerEmail, string customerFullName);
        Task<CustomerProfiles> UpdateCustomerProfile(string customerEmail, CustomerProfiles customerProfile);
        Task<List<CustomerProfiles>> GetCustomerProfiles(); //Ienumerable

    }
}
