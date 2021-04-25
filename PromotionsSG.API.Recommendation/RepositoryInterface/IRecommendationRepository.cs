using Common.DBTableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionsSG.API.Recommendation.RepositoryInterface
{
    public interface IRecommendationRepository
    {
        Task<int> CreateRecommendation(Recommendations recommendation);
        Task<Recommendations> Recommendation(int recommendationId);
        Task<IEnumerable<Recommendations>> GetRecommendations();
        Task<int> UpdateRecommendation(Recommendations recommendation);
        Task<IEnumerable<CodeTable>> GetCentralRegion();
        Task<IEnumerable<CodeTable>> GetEastRegion();
        Task<IEnumerable<CodeTable>> GetNorthRegion();
        Task<IEnumerable<CodeTable>> GetNERegion();
        Task<IEnumerable<CodeTable>> GetWestRegion();

    }
}