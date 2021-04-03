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

            public static string RetrieveLoginAPI(string api) => $"{BaseURI}/{api}";
            public static string InsertLoginAPI(string api) => $"{BaseURI}/{api}";
            public static string UpdateLoginAPI(string api) => $"{BaseURI}/{api}";
        }

        public static class CustomerProfile
        {
            public static string BaseURI { get; set; }

            public static string RetrieveCustomerProfileAPI(string api) => $"{BaseURI}/{api}";
            public static string InsertCustomerProfileAPI(string api) => $"{BaseURI}/{api}";
            public static string UpdateCustomerProfileAPI(string api) => $"{BaseURI}/{api}";
        }

        public static class ShopProfile
        {
            public static string BaseURI { get; set; }

            public static string RetrieveShopProfileAPI(string api) => $"{BaseURI}/{api}";
            public static string InsertShopProfileAPI(string api) => $"{BaseURI}/{api}";
            public static string UpdateShopProfileAPI(string api) => $"{BaseURI}/{api}";
        }
    }
}
