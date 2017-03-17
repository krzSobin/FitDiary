using FitDiary.Api.DAL;
using FitDiary.Api.Models;
using FitDiary.Contracts.DTOs.Diet;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FitDiary.Api.Services
{
    public interface IMealService
    {
        IEnumerable<MealDTO> GetMeals();
        Task<Meal> GetMealAsync(int id);
        IEnumerable<MealDTO> GetMeals(DateTime date);
    }
    public class MealService : IMealService
    {
        private readonly FitDiaryApiContext _db;

        public MealService(FitDiaryApiContext dbcontext)
        {
            _db = dbcontext;
        }

        public IEnumerable<MealDTO> GetMeals()
        {
            var meals = _db.Meals.Select(m =>
            new MealDTO
            {
                Id = m.Id,
                Date = m.Date,
                TotalKcal = m.TotalKcal,
                TotalProtein = m.TotalProtein,
                TotalFat = m.TotalFat,
                TotalCarb = m.TotalCarb,
                TotalSugar = m.TotalSugar
            });
            var mealsList = meals.ToList<MealDTO>();

            return mealsList;
        }

        public async Task<Meal> GetMealAsync(int id) //TODO zmienic na mealDTO
        {
            var meal = await _db.Meals.FindAsync(id);

            return meal;
        }

        public IEnumerable<MealDTO> GetMeals(DateTime date)
        {
            var meals = _db.Meals.Select(m =>
            new MealDTO
            {
                Id = m.Id,
                Date = m.Date,
                TotalKcal = m.TotalKcal,
                TotalProtein = m.TotalProtein,
                TotalFat = m.TotalFat,
                TotalCarb = m.TotalCarb,
                TotalSugar = m.TotalSugar
            })
            .Where(m => DbFunctions.TruncateTime(m.Date) == date);
            var mealsList = meals.ToList<MealDTO>();

            return mealsList;
        }
    }
}