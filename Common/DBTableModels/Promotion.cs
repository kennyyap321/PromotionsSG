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
        
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        public int Qty { get; set; }
        public string Region { get; set; }
        public bool IsActive { get; set; }
    }
}
