using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Common.DBTableModels
{
    public class User
    {
        [Key]
        public int UserId{ get; set; }

        [Required]
        [DisplayName("Shop / Customer")]
        public int UserType { get; set; }

        [Required]
        [DisplayName("Email")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
