using System;
using System.Collections.Generic;

namespace FitDiary.Contracts.DTOs.Diet.Meals
{
    public class UpdateMealDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public int UserId { get; set; }

        public IEnumerable<ProductInMealForUpdateMealDTO> Products { get; set; }
    }
}
