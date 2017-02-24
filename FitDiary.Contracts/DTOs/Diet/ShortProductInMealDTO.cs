using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitDiary.Contracts.DTOs.Diet
{
    public class ShortProductInMealDTO
    {
        public string Name { get; set; }
        public int ProductInMealId { get; set; }
        public double Amount { get; set; }

        public double TotalKcal { get; set; }
        public double TotalProteins { get; set; }
        public double TotalFats { get; set; }
        public double TotalCarbs { get; set; }
        public double TotalSugar { get; set; }
    }
}
