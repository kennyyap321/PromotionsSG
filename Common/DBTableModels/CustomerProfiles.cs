using System;
using System.ComponentModel.DataAnnotations;

namespace Common.DBTableModels
{
    public class CustomerProfiles
    {
        [Key]
        public int CustomerProfileId { get; set; }
        [Required]
        public string CustomerFullName { get; set; }
        [Required]
        public string CustomerAddress { get; set; }
        [Required]
        public string CustomerEmail { get; set; }
        [Required]
        public string CustomerPhone { get; set; }
        public string CustomerType { get; set; }
        [Required]
        public string CustomerGender { get; set; }
        public bool CustomerActive { get; set; } //[CustomerActive] BIT NULL,

        [Required]
        [Display(Name = "CustomerDOB", Prompt = "e.g. 2000-01-22")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? CustomerDOB { get; set; }
        public string CreatedBy { get; set; }  //    [CreatedBy] VARCHAR(50) NULL,
        public DateTime? CreatedTime { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedTime { get; set; }
        public int VersionNo { get; set; }
        public bool IsDeleted { get; set; }
    }
}

