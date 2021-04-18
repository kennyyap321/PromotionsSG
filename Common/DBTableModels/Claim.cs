using System;
using System.ComponentModel.DataAnnotations;

namespace Common.DBTableModels
{
    public class Claim
    {
        [Key]
        public int ClaimId { get; set; }

        [Required]
        public int CustomerProfileId { get; set; }

        [Required]
        public int PromotionId { get; set; }

        public DateTime ClaimDate { get; set; }

        public bool Consumed { get; set; }

        public DateTime? ConsumeDate { get; set; }
    }
}
