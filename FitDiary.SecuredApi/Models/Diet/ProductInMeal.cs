using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitDiary.SecuredApi.Models.Diet
{
    public class ProductInMeal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public double AmountInGrams { get; set; }
        public int ProductId { get; set; }
        public int MealId { get; set; }

        public virtual FoodProduct Product { get; set; }
        public virtual Meal Meal { get; set; }
    }
}