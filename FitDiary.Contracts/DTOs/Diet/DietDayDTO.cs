using System;

namespace FitDiary.Contracts.DTOs.Diet
{
    public class DietDayDTO
    {
        public DateTime Date { get; set; }
        public int MealsCount { get; set; }
        public double TotalKCal { get; set; }
        public double TotalProteins { get; set; }
        public double TotalCarbs { get; set; }
        public double TotalFats { get; set; }
        public double TotalSugar { get; set; }
        public double RealizationPercent { get; set; }
    }
}
