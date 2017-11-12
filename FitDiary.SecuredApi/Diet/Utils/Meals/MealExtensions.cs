using FitDiary.Contracts.DTOs.Diet;
using FitDiary.Contracts.DTOs.Diet.Meals;
using FitDiary.SecuredApi.Models.Diet;
using System.Collections.Generic;

namespace FitDiary.SecuredApi.Diet.Utils.Meals
{
    public static class MealExtensions
    {
        public static void SetTotalMacros(this MealForListingDTO meal, IEnumerable<ProductInMeal> products)
        {
            var kCalTotalSum = 0.0;
            var proteinTotalSum = 0.0;
            var fatTotalSum = 0.0;
            var carbtotalSum = 0.0;
            foreach (var productInMeal in products)
            {
                kCalTotalSum += productInMeal.AmountInGrams * productInMeal.Product.KCalPer100g / 100;
                proteinTotalSum += productInMeal.AmountInGrams * productInMeal.Product.ProteinsPer100g / 100;
                fatTotalSum += productInMeal.AmountInGrams * productInMeal.Product.FatsPer100g / 100;
                carbtotalSum += productInMeal.AmountInGrams * productInMeal.Product.CarbsPer100g / 100;
            }

            meal.TotalKcal = kCalTotalSum;
            meal.TotalProtein = proteinTotalSum;
            meal.TotalCarb = carbtotalSum;
            meal.TotalFat = fatTotalSum;
        }

        public static void SetTotalMacros(this MealsDailyDTO mealsDaily, IEnumerable<MealForListingDTO> meals)
        {
            var kCalTotalSum = 0.0;
            var proteinTotalSum = 0.0;
            var fatTotalSum = 0.0;
            var carbTotalSum = 0.0;
            foreach (var meal in meals)
            {
                kCalTotalSum += meal.TotalKcal;
                proteinTotalSum += meal.TotalProtein;
                fatTotalSum += meal.TotalFat;
                carbTotalSum += meal.TotalCarb;
            }

            mealsDaily.TotalKcal = kCalTotalSum;
            mealsDaily.TotalProtein = proteinTotalSum;
            mealsDaily.TotalCarb = carbTotalSum;
            mealsDaily.TotalFat = fatTotalSum;
        }
    }
}