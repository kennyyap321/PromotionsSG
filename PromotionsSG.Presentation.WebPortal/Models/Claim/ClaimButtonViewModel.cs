using Common.DBTableModels;
using System;
using System.Collections.Generic;

namespace PromotionsSG.Presentation.WebPortal.Models
{
    public class ClaimButtonViewModel
    {
        public int? UserId { get; set; }
        public int PromotionId { get; set; }
        public bool InitializeSection { get; set; }
    }
}
