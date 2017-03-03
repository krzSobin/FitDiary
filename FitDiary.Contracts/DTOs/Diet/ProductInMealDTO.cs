using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitDiary.Contracts.DTOs.Diet
{
    public class ProductInMealDTO
    {
        public int Id { get; set; }
        public int MealId { get; set; }
        public double AmountInGrams { get; set; }
        public FoodProductDTO Product { get; set; }

        public double Kcal
        {
            get { return AmountInGrams * Product.KCalPer100g / 100; }
        }
        public double Proteins
        {
            get { return AmountInGrams * Product.KCalPer100g / 100; }
        }
        public double Fats
        {
            get { return AmountInGrams * Product.FatsPer100g / 100; }
        }
        public double Carbs
        {
            get { return AmountInGrams * Product.CarboPer100g / 100; }
        }
        public double Sugar
        {
            get { return AmountInGrams * Product.SugarPer100g / 100; }
        }
    }
}
