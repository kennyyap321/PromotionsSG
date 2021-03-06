using System;
using System.Collections.Generic;
using System.Text;

namespace Common.AppSettings
{
    public static class URLConfig
    {
        public static class Login
        {
            public static string BaseURI { get; set; }

            public static string LoginAPI(string api) => $"{BaseURI}/{api}";
            //public static string RetrieveLoginAPI(string api) => $"{BaseURI}/{api}";
            //public static string InsertLoginAPI(string api) => $"{BaseURI}/{api}";
            //public static string UpdateLoginAPI(string api) => $"{BaseURI}/{api}";
        }

        public static class CustomerProfile
        {
            public static string BaseURI { get; set; }

            public static string RetrieveCustomerProfileAPI(string api) => $"{BaseURI}/{api}";
            public static string RetrieveAllCustomersProfileAPI(string api) => $"{BaseURI}/{api}";
            public static string InsertCustomerProfileAPI(string api) => $"{BaseURI}/{api}";
            public static string UpdateCustomerProfileAPI(string api) => $"{BaseURI}/{api}";
            public static string UpdateCustomerAPI(string api) => $"{BaseURI}/{api}";
        }

        public static class ShopProfile
        {
            public static string BaseURI { get; set; }
            public static string ShopProfileAPI(string api) => $"{BaseURI}/{api}";
            //public static string RetrieveShopProfileAPI(string api) => $"{BaseURI}/{api}";
            //public static string InsertShopProfileAPI(string api) => $"{BaseURI}/{api}";
            //public static string UpdateShopProfileAPI(string api) => $"{BaseURI}/{api}";
        }

        public static class Promotion
        {
            public static string BaseURI { get; set; }
            public static string RetrieveAllPromotionsAPI(string api) => $"{BaseURI}/{api}";
            public static string RetrievePromotionByShopIdAPI(string api) => $"{BaseURI}/{api}";
            public static string RetrievePromotionAPI(string api) => $"{BaseURI}/{api}";
            public static string InsertPromotionAPI(string api) => $"{BaseURI}/{api}";
            public static string UpdatePromotionAPI(string api) => $"{BaseURI}/{api}";
            public static string DeletePromotionAPI(string api) => $"{BaseURI}/{api}";
            public static string SearchPromotionsAPI(string api) => $"{BaseURI}/{api}";
            public static string RetrievePromotionsByPromotionIdsAPI(string api) => $"{BaseURI}/{api}";
            public static string RetrievePromotionByRegionAPI(string api) => $"{BaseURI}/{api}";
            public static string RetrieveNewestPromotionsAPI(string api) => $"{BaseURI}/{api}";
        }

        public static class Claim
        {
            public static string BaseURI { get; set; }
            public static string ClaimAPI(string api) => $"{BaseURI}/{api}";
            public static string RetrieveAllClaimsByPromotionAPI(string api) => $"{BaseURI}/{api}";
        }

        public static class Feedback
        {
            public static string BaseURI { get; set; }

            public static string RetrieveFeedbackAPI(string api) => $"{BaseURI}/{api}";
            public static string RetrieveAllFeedbackAPI(string api) => $"{BaseURI}/{api}";
            public static string InsertFeedbackAPI(string api) => $"{BaseURI}/{api}";
            public static string UpdateFeedbackAPI(string api) => $"{BaseURI}/{api}";
        }

        public static class Recommendation
        {
            public static string BaseURI { get; set; }

            public static string RetrieveRecommendationAPI(string api) => $"{BaseURI}/{api}";
            public static string RetrieveAllRecommendationsAPI(string api) => $"{BaseURI}/{api}";
            public static string InsertRecommendationAPI(string api) => $"{BaseURI}/{api}";
            public static string UpdateRecommendationAPI(string api) => $"{BaseURI}/{api}";
            public static string RetrieveAllCentralRegionByPostalCodeAPI(string api) => $"{BaseURI}/{api}";
            public static string RetrieveAllEastRegionByPostalCodeAPI(string api) => $"{BaseURI}/{api}";
            public static string RetrieveAllNorthRegionByPostalCodeAPI(string api) => $"{BaseURI}/{api}";
            public static string RetrieveAllNERegionByPostalCodeAPI(string api) => $"{BaseURI}/{api}";
            public static string RetrieveAllWestRegionByPostalCodeAPI(string api) => $"{BaseURI}/{api}";

        }

        public static class Notification
        {
            public static string BaseURI { get; set; }

            public static string SendNotificationAPI(string api) => $"{BaseURI}/{api}";

        }
    }
}
