using Common.DBTableModels;
using System;
using System.Collections.Generic;

namespace PromotionsSG.Presentation.WebPortal.Models
{
    public class ClaimViewModel
    {
        public Claim ClaimDto { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
    }
}
