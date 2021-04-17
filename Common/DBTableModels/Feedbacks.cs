using System;
using System.ComponentModel.DataAnnotations;

namespace Common.DBTableModels
{
    public class Feedbacks
    {
        [Key]
        public int FeedbackId { get; set; }
        public int PromotionId { get; set; }
        public int ClaimId { get; set; }
        public int CustomerId { get; set; }
        public string Title { get; set; } //[CustomerActive] BIT NULL,
        public string Description { get; set; }
        public string Rating { get; set; }
        public DateTime? CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}

