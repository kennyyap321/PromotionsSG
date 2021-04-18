using Common.DBTableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionsSG.Presentation.WebPortal
{
    public interface IFeedbackService
    {
        Task<Feedbacks> Feedback(int promotionId);
        Task<List<Feedbacks>> GetFeedbacks();
        Task<int> CreateFeedback(Feedbacks feedback);
        Task<int> UpdateFeedback(Feedbacks feedback);

    }
}
