using Common.DBTableModels;
using System;
using System.Collections.Generic;

namespace PromotionsSG.Presentation.WebPortal.Models
{
    public class FeedbackViewModel
    {
        public class Feedback
        {
            public int FeedbackId { get; set; }
            public int PromotionId { get; set; }
            public int ClaimId { get; set; }
            public int CustomerId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Rating { get; set; }
            public DateTime? CreatedTime { get; set; }
            public bool IsDeleted { get; set; }
        }

        public Feedbacks feedbackDto { get; set; }
        public int FeedbackId { get; set; }
        public int PromotionId { get; set; }
        public int ClaimId { get; set; }
        public int CustomerId { get; set; }
        public string Title { get; set; } 
        public string Description { get; set; }
        public string Rating { get; set; }
        public DateTime? CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
        public List<Feedbacks> allFeedbacks { get; set; }

    }
}
