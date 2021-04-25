using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.DBTableModels
{
    public class Recommendations
    {
        [Key]
        public int RecommendationId { get; set; }
        public int CustomerId { get; set; }
        public int PromotionId { get; set; }
        public int ShopId { get; set; }
        public int FeedbackId { get; set; }
        public int ClaimId { get; set; }
        public string Region { get; set; }
        public int NoOfClicks { get; set; }
        public string Category { get; set; }
    }
}
