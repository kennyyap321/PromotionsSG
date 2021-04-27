using Common.DBTableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionsSG.Presentation.WebPortal
{
    public interface IRecommendationService
    {
        //Task<CustomerProfiles> CustomerProfile(string customerEmail);
        //Task<string> CreateCustomer(CustomerProfiles customer);
        //Task<string> UpdateCustomer(CustomerProfiles customer);
        Task<List<CodeTable>> GetCentralRegionByPostalCode();
        Task<List<CodeTable>> GetEastRegionByPostalCode();
        Task<List<CodeTable>> GetNorthRegionByPostalCode();
        Task<List<CodeTable>> GetNERegionByPostalCode();
        Task<List<CodeTable>> GetWestRegionByPostalCode();
        Task<List<Claim>> GetClaimCountByPromotion();
    }
}
