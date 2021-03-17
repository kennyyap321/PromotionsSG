using System;
using System.ComponentModel.DataAnnotations;

namespace Common.DBTableModels
{
    public class UserLogin
    {
        [Key]
        public string UserId { get; set; }
        public string UserLoginId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
