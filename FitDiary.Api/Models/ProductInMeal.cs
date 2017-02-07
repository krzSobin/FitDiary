using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitDiary.Api.Models
{
    public class ProductInMeal
    {
        public int Id { get; set; }
        public double AmountInGrams { get; set; }
        public int ProductId { get; set; }
        public int MealId { get; set; }

        public virtual FoodProduct Product { get; set; }
        public virtual Meal Meal { get; set; }
    }
}