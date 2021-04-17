using System;
using System.ComponentModel.DataAnnotations;

namespace Common.DBTableModels
{
    public class User
    {
        [Key]
        public int UserId{ get; set; }

        public int UserType { get; set; }

        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
