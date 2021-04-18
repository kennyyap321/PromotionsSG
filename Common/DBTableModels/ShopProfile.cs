using System;
using System.ComponentModel.DataAnnotations;

namespace Common.DBTableModels
{
    public class ShopProfile
    {
        [Key]
        public int ShopProfileId { get; set; }
        public int UserId { get; set; }
        public string ShopName { get; set; }
        public string ShopAddress { get; set; }
    }
}
