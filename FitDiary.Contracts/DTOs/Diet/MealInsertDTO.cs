using System;
using System.Collections.Generic;

namespace FitDiary.Contracts.DTOs.Diet
{
    public class MealInsertDTO
    {
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public IEnumerable<ProductInMealInsertDTO> Products { get; set; }
    }
}
