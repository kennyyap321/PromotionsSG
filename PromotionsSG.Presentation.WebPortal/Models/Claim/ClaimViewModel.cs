using Common.DBTableModels;
using System;
using System.Collections.Generic;

namespace PromotionsSG.Presentation.WebPortal.Models
{
    public class ClaimViewModel
    {
        public ClaimWithPromotionAndShopInfo ClaimExtraInfo { get; set; }
        public IEnumerable<ClaimWithPromotionAndShopInfo> ClaimExtraInfoListDto { get; set; }
        public byte[] QrBtyes { get; set; }
    }

    public class ClaimWithPromotionAndShopInfo
    {
        public Claim ClaimDto { get; set; }
        public Promotion PromotionDto { get; set; }
        public ShopProfile ShopProfileDto { get; set; }
    }
}
