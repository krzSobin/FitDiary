using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitDiary.Api.Diet.Models
{
    public class FoodProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public double ProteinsPer100g { get; set; }
        public double FatsPer100g { get; set; }
        public double CarboPer100g { get; set; }
        public double SugarPer100g { get; set; }
        public double KCalPer100g { get; set; }

        public int CategoryId { get; set; }
        public FoodProductCategory Category { get; set; }

        //public virtual ICollection<ProductInMeal> Products { get; set; }
    }
}