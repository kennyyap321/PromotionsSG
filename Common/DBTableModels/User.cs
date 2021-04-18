using System;
using System.ComponentModel.DataAnnotations;

namespace Common.DBTableModels
{
    public class User
    {
        [Key]
        public int UserId{ get; set; }

        [Required]
        public int UserType { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
