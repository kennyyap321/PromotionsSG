using System;
using System.Collections.Generic;
using System.Text;

namespace Common.AppSettings
{
    public class APIUrls
    {
        #region LoginAPI
        public string LoginAPI_Base { get; set; }
        public string LoginAPI_Retrieve { get; set; }
        public string LoginAPI_Insert { get; set; }
        public string LoginAPI_Update { get; set; }
        public string LoginAPI_Login { get; set; }
        #endregion


        #region CustomerProfileAPI
        public string CustomerProfileAPI_Base { get; set; }
        public string CustomerProfileAPI_RetrieveAll { get; set; }
        public string CustomerProfileAPI_Retrieve { get; set; }
        public string CustomerProfileAPI_Insert { get; set; }
        public string CustomerProfileAPI_Update { get; set; }
        #endregion


        #region ShopProfileAPI
        public string ShopProfileAPI_Base { get; set; }
        public string ShopProfileAPI_Retrieve { get; set; }
        public string ShopProfileAPI_Insert { get; set; }
        public string ShopProfileAPI_Update { get; set; }
        public string ShopProfileAPI_RetrieveShopProfileByUserId { get; set; }
        public string ShopProfileAPI_RetrieveShopProfilesByShopProfileIds { get; set; }
        #endregion


        #region PromotionAPI
        public string PromotionAPI_Base { get; set; }
        public string PromotionAPI_RetrieveAll { get; set; }
        public string PromotionAPI_Retrieve { get; set; }
        public string PromotionAPI_RetrieveByShopId { get; set; }
        public string PromotionAPI_Insert { get; set; }
        public string PromotionAPI_Update { get; set; }
        public string PromotionAPI_Delete { get; set; }
        public string PromotionAPI_Search { get; set; }
        public string PromotionAPI_RetrievePromotionsByPromotionIds { get; set; }
        public string PromotionAPI_RetrieveByRegion { get; set; }
        public string PromotionAPI_RetrieveNewestPromotions { get; set; }
        #endregion


        #region ClaimAPI
        public string ClaimAPI_Base { get; set; }
        public string ClaimAPI_Retrieve { get; set; }
        public string ClaimAPI_RetrieveAll { get; set; }
        public string ClaimAPI_Insert { get; set; }
        public string ClaimAPI_Update { get; set; }
        public string ClaimAPI_Claim { get; set; }
        public string ClaimAPI_RetrieveByCustomerProfileId { get; set; }
        public string ClaimAPI_RetrieveByPromotion { get; set; }
        #endregion


        #region FeedbackAPI
        public string FeedbackAPI_Base { get; set; }
        public string FeedbackAPI_RetrieveAll { get; set; }
        public string FeedbackAPI_Retrieve { get; set; }
        public string FeedbackAPI_Insert { get; set; }
        public string FeedbackAPI_Update { get; set; }
        #endregion


        #region RecommendationAPI
        public string RecommendationAPI_Base { get; set; }
        public string RecommendationAPI_Retrieve { get; set; }
        public string RecommendationAPI_RetrieveAll { get; set; }
        public string RecommendationAPI_Insert { get; set; }
        public string RecommendationAPI_Update { get; set; }
        public string RecommendationAPI_RetrieveCentralPostalCode { get; set; }
        public string RecommendationAPI_RetrieveEastPostalCode { get; set; }
        public string RecommendationAPI_RetrieveNorthPostalCode { get; set; }
        public string RecommendationAPI_RetrieveNEPostalCode { get; set; }
        public string RecommendationAPI_RetrieveWestPostalCode { get; set; }
        #endregion

        #region NotificationAPI
        public string NotificationAPI_Base { get; set; }
        public string NotificationAPI_SendEmail { get; set; }
        public string NotificationAPI_PublishEmail { get; set; }
        #endregion
    }
}
