using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitDiary.Contracts.DTOs.Diet
{
    public class MealInsertOrUpdateDTO
    {
        public string Name { get; set; }
        
        public DateTime Date { get; set; }

        public int? UserId { get; set; }

        public IEnumerable<ProductInMealInsertOrUpdateDTO> Products { get; set; }
    }
}
