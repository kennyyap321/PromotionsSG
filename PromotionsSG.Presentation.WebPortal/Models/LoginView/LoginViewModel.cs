using Common.DBTableModels;
using System;

namespace PromotionsSG.Presentation.WebPortal.Models
{
    public class LoginViewModel
    {
        public UserLogin userLoginDto { get; set; }
        public string userName { get; set; }
        public string password { get; set; }

    }
}
