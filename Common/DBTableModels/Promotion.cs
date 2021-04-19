using System;
using System.ComponentModel.DataAnnotations;

namespace Common.DBTableModels
{
    public class Promotion
    {
        [Key]
        public int PromotionId { get; set; }
        public int ShopProfileId { get; set; }
        public int Type { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EndDate { get; set; }

        public int Qty { get; set; }
        public bool IsActive { get; set; }
    }
}
