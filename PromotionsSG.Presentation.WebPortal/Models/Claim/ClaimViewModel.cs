using Common.DBTableModels;
using System;
using System.Collections.Generic;

namespace PromotionsSG.Presentation.WebPortal.Models
{
    public class ClaimViewModel
    {
        public ClaimWithPromotionAndShopInfo ClaimDto { get; set; }
        public IEnumerable<ClaimWithPromotionAndShopInfo> ClaimListDto { get; set; }
    }

    public class ClaimWithPromotionAndShopInfo
    {
        public Claim ClaimDto { get; set; }
        public Promotion PromotionDto { get; set; }
        public ShopProfile ShopProfileDto { get; set; }
    }
}
