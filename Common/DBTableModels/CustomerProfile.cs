using System;
using System.ComponentModel.DataAnnotations;

namespace Common.DBTableModels
{
    public class CustomerProfile
    {
        [Key]
        public string CustomerProfileId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
    }
}
