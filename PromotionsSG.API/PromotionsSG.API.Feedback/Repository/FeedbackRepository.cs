using Common.DBTableModels;
using PromotionsSG.API.Feedback.RepositoryInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionsSG.API.Feedback.Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly MyDBContext _context;

        public FeedbackRepository(MyDBContext context)
        {
            _context = context;
        }
        public async Task<int> CreateFeedback(Feedbacks feedbacks)
        {
            _context.Feedback.Add(feedbacks);
            var result = await _context.SaveChangesAsync();

            var createdFeedback = (await _context.Feedback.FirstAsync(s => s.PromotionId == feedbacks.PromotionId)).PromotionId;
            return createdFeedback;
        }

        public async Task<Feedbacks> Feedback(int promotionid)
        {
            //use .where for multiple records
            var feedbackData = await _context.Feedback.FirstOrDefaultAsync(x => x.PromotionId == promotionid);
            return feedbackData;
        }

        public async Task<IEnumerable<Feedbacks>> GetFeedbacks()
        {
            var feedbackData = await _context.Feedback.Where(x => x.IsDeleted == false).ToListAsync();
            return feedbackData;
        }

        public async Task<int> UpdateFeedback(Feedbacks feedbacks)
        {
            _context.Feedback.Update(feedbacks);
            var result = await _context.SaveChangesAsync();

            var updatedFeedbackData = feedbacks.PromotionId;

            return updatedFeedbackData;
        }
    }
}
