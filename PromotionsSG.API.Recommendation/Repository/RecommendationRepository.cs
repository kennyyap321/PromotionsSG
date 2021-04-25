using Common.DBTableModels;
using PromotionsSG.API.Recommendation.RepositoryInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionsSG.API.Recommendation.Repository
{
    public class RecommendationRepository : IRecommendationRepository
    {
        private readonly MyDBContext _context;

        public RecommendationRepository(MyDBContext context)
        {
            _context = context;
        }
        public async Task<int> CreateRecommendation(Recommendations recommendation)
        {
            _context.Add(recommendation);
            var result = await _context.SaveChangesAsync();

            var createdRecommendationEmail = (await _context.Recommendation.FirstAsync(s => s.RecommendationId == recommendation.RecommendationId)).RecommendationId;
            return createdRecommendationEmail;
        }

        public async Task<Recommendations> Recommendation(int recommendationId)
        {
            //use .where for multiple records
            var recommendationData = await _context.Recommendation.FirstOrDefaultAsync(x => x.RecommendationId == recommendationId);
            return recommendationData;
        }

        public async Task<IEnumerable<Recommendations>> GetRecommendations()
        {
            var recommendationData = await _context.Recommendation.Where(x => x.RecommendationId < 0).ToListAsync();
            return recommendationData;
        }

        public async Task<int> UpdateRecommendation(Recommendations recommendations)
        {
            _context.Recommendation.Update(recommendations);
            var result = await _context.SaveChangesAsync();

            var updatedRecommendationEmail = recommendations.RecommendationId;

            return updatedRecommendationEmail;
        }

        #region get region by postal code
        public async Task<IEnumerable<CodeTable>> GetCentralRegion()
        {
            var regionData = await _context.CodeTable.Where(x => x.CodeDesc == "Central").ToListAsync();
            return regionData;
        }

        public async Task<IEnumerable<CodeTable>> GetEastRegion()
        {
            var regionData = await _context.CodeTable.Where(x => x.CodeDesc == "East").ToListAsync();
            return regionData;
        }

        public async Task<IEnumerable<CodeTable>> GetNorthRegion()
        {
            var regionData = await _context.CodeTable.Where(x => x.CodeDesc == "North").ToListAsync();
            return regionData;
        }
        public async Task<IEnumerable<CodeTable>> GetNERegion()
        {
            var regionData = await _context.CodeTable.Where(x => x.CodeDesc == "North-East").ToListAsync();
            return regionData;
        }
        public async Task<IEnumerable<CodeTable>> GetWestRegion()
        {
            var regionData = await _context.CodeTable.Where(x => x.CodeDesc == "West").ToListAsync();
            return regionData;
        }
        #endregion
    }
}
