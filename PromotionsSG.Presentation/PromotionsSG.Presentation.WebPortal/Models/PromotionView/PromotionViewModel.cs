using Common.DBTableModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace PromotionsSG.Presentation.WebPortal.Models
{
    public class PromotionViewModel
    {
        public Promotion PromotionDto { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public List<Promotion> Promotions { get; set; }
    }
}
