using Common.DBTableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionsSG.API.Feedback.RepositoryInterface
{
    public interface IFeedbackRepository
    {
        Task<int> CreateFeedback(Feedbacks feedbacks);
        Task<Feedbacks> Feedback(int promotionid);
        Task<IEnumerable<Feedbacks>> GetFeedbacks();
        Task<int> UpdateFeedback(Feedbacks feedbacks);

    }
}