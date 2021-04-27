using Common.DBTableModels;
using System;
using System.Collections.Generic;

namespace PromotionsSG.Presentation.WebPortal.Models
{
    public class RecommendationViewModel
    {
        public Recommendations recommendationDto { get; set; }
        public int RecommendationId { get; set; }
        public int CustomerId { get; set; }
        public int PromotionId { get; set; }
        public int ShopId { get; set; }
        public int FeedbackId { get; set; }
        public int ClaimId { get; set; }
        public string Region { get; set; }
        public int NoOfClicks { get; set; }
        public string Category { get; set; }
        public List<Recommendations> allRecommendations { get; set; }
        public CustomerProfiles customerDto { get; set; }
        public List<Promotion> AllPromotionsByRegion { get; set; }
        public List<Promotion> AllNewPromotions { get; set; }
        public List<Promotion> AllPopularPromotions { get; set; }
        public List<Claim> AllClaimsByPromotions { get; set; }
        public Promotion TopPromotion { get; set; }


    }
}
