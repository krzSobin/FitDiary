using System;
using System.Collections.Generic;

namespace FitDiary.Contracts.DTOs.Diet
{
    public class MealInsertOrUpdateDTO
    {
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public IEnumerable<ProductInMealInsertOrUpdateDTO> Products { get; set; }
    }
}
