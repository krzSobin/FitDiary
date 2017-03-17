using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitDiary.Api.Models.QueryModels
{
    public class FoodProductQuery
    {
        public string Category { get; set; }
        public string Name { get; set; }
        public double? MaxSugar { get; set; }
        public string SortField { get; set; } = nameof(Name);
    }
}