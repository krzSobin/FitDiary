using System;
using System.Collections.Generic;

namespace FitDiary.Contracts.DTOs.Diet.Meals
{
    public class MealsDailyDTO
    {
        public DateTime Date { get; set; }
        public IList<MealForListingDTO> Meals { get; set; }
        public double TotalKcal { get; set; }
        public double TotalProtein { get; set; }
        public double TotalFat { get; set; }
        public double TotalCarb { get; set; }
        public double TotalSugar { get; set; }

        public MealsDailyDTO(DateTime date, MealForListingDTO meal)
        {
            Date = date;
            Meals = new List<MealForListingDTO>
            {
                meal
            };
        }
    }
}
