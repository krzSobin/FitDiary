using System;
using System.Collections.Generic;

namespace FitDiary.Contracts.DTOs.Diet.Meals
{
    public class MealInsertOrUpdateDTO
    {
        public string Name { get; set; }
        
        public DateTime Date { get; set; }

        public int? UserId { get; set; }

        public IEnumerable<ProductInMealInsertOrUpdateDTO> Products { get; set; }
    }
}
