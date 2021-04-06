using Common.DBTableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionsSG.API.CustomerProfile.RepositoryInterface
{
    public interface ICustomerProfileRepository
    {
        Task<int> CreateCustomerProfile(CustomerProfiles customerProfile);
        Task<CustomerProfiles> UpdateCustomerProfile(CustomerProfiles customerProfile);
        Task<CustomerProfiles> Customer(string customerProfileEmail);
        Task<IEnumerable<CustomerProfiles>> GetCustomerProfiles();

    }
}