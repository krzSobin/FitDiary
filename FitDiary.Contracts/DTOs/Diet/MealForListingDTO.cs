using System;

namespace FitDiary.Contracts.DTOs.Diet
{
    public class MealForListingDTO
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public double TotalKcal { get; set; }
        public double TotalProtein { get; set; }
        public double TotalFat { get; set; }
        public double TotalCarb { get; set; }
        public double TotalSugar { get; set; }

        //public IEnumerable<ShortProductInMealDTO> Products { get; set; }
    }
}
