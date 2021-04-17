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
        #endregion


        #region PromotionAPI
        public string PromotionAPI_Base { get; set; }
        public string PromotionAPI_RetrieveAll { get; set; }
        public string PromotionAPI_Retrieve { get; set; }
        public string PromotionAPI_RetrieveByShopId { get; set; }
        public string PromotionAPI_Insert { get; set; }
        public string PromotionAPI_Update { get; set; }
        public string PromotionAPI_Search { get; set; }
        #endregion

        #region FeedbackAPI
        public string FeedbackAPI_Base { get; set; }
        public string FeedbackAPI_RetrieveAll { get; set; }
        public string FeedbackAPI_Retrieve { get; set; }
        public string FeedbackAPI_Insert { get; set; }
        public string FeedbackAPI_Update { get; set; }
        #endregion
    }
}
