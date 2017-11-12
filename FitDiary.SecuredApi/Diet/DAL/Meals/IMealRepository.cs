using FitDiary.Contracts.DTOs.Diet;
using FitDiary.SecuredApi.Models.Diet;
using System;
using System.Collections.Generic;

namespace FitDiary.SecuredApi.Diet.DAL.Meals
{
    public interface IMealRepository : IDisposable
    {
        IEnumerable<Meal> GetMeals(int userId);
        IEnumerable<MealForListingDTO> GetMealsByDay();
        Meal GetMealById(int id);
        IEnumerable<Meal> GetMealsByDate(DateTime date, int userId);
        Meal AddMeal(Meal meal);
        void UpdateMeal(Meal meal);
        void DeleteMeal(Meal meal);
        bool Save();
    }
}
