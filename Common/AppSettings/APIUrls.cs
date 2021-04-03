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
        #endregion


        #region CustomerProfileAPI
        public string CustomerProfileAPI_Base { get; set; }

        public string CustomerProfileAPI_Retrieve { get; set; }
        public string CustomerProfileAPI_Insert { get; set; }
        public string CustomerProfileAPI_Update { get; set; }
        #endregion


        #region ShopProfileAPI
        public string ShopProfileAPI_Base { get; set; }

        public string ShopProfileAPI_Retrieve { get; set; }
        public string ShopProfileAPI_Insert { get; set; }
        public string ShopProfileAPI_Update { get; set; }
        #endregion
    }
}
