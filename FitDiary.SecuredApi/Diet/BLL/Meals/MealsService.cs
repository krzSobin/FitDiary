using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using FitDiary.Contracts.DTOs.Diet;
using FitDiary.SecuredApi.Diet.DAL.Meals;
using FitDiary.SecuredApi.Models.Diet;
using AutoMapper;
using FitDiary.Contracts.DTOs.Diet.Meals;
using FitDiary.SecuredApi.Diet.Utils.Meals;
using FitDiary.SecuredApi.Diet.DAL.ProductsInMeal;
using FitDiary.SecuredApi.Diet.DAL.FoodProducts;

namespace FitDiary.SecuredApi.Diet.BLL.Meals
{
    public class MealsService
    {
        private readonly IMealRepository _mealRepository;
        private readonly IFoodProductRepository _foodProductRepository;
        private readonly IProductInMealRepository _productInMealRepository;

        public MealsService(IMealRepository mealRepository, IFoodProductRepository foodProductRepository, IProductInMealRepository productInMealRepository)
        {
            _mealRepository = mealRepository;
            _foodProductRepository = foodProductRepository;
            _productInMealRepository = productInMealRepository;
        }

        public IEnumerable<MealsDailyDTO> GetMeals(int userId)
        {
            var mealEntities = _mealRepository.GetMeals(userId);

            var mealsForListing = Mapper.Map<IEnumerable<MealForListingDTO>>(mealEntities).OrderBy(m => m.Date);
            foreach (var meal in mealsForListing)
            {
                var mealEntity = mealEntities.FirstOrDefault(m => m.Id == meal.Id);
                meal.SetTotalMacros(mealEntity.Products);
            }

            var dailyMeals = new List<MealsDailyDTO>();
            foreach (var meal in mealsForListing)
            {
                var mealInDay = dailyMeals.FirstOrDefault(m => m.Date.Date == meal.Date.Date);
                if (mealInDay != null)
                    mealInDay.Meals.Add(meal);
                else
                    dailyMeals.Add(new MealsDailyDTO(meal.Date.Date, meal));
            }
            foreach (var meal in dailyMeals)
                meal.SetTotalMacros(meal.Meals);

            return dailyMeals;
        }

        public IEnumerable<MealForListingDTO> GetMealsByDate(DateTime date, int userId)
        {
            var meals = _mealRepository.GetMealsByDate(date, userId);
            if (meals == null)
                return null;

            var mealDTOs = Mapper.Map<IEnumerable<MealForListingDTO>>(meals);

            foreach (var mealDTO in mealDTOs)
            {
                var mealEntity = meals.FirstOrDefault(m => m.Id == mealDTO.Id);
                mealDTO.SetTotalMacros(mealEntity.Products);
            }

            return mealDTOs;
        }

        public MealForListingDTO GetMealById(int id)
        {
            var meal = _mealRepository.GetMealById(id);

            var mealDTO = Mapper.Map<MealForListingDTO>(meal);

            mealDTO.SetTotalMacros(meal.Products);

            return mealDTO;
        }

        public AddMealResultDTO AddMeal(MealInsertOrUpdateDTO mealDTO)
        {
            var createResult = new AddMealResultDTO
            {
                Added = false,
                Meal = null
            };

            var meal = Mapper.Map<Meal>(mealDTO);

            var createdMeal = _mealRepository.AddMeal(meal);
            if (_mealRepository.Save())
            {
                createResult.Added = true;
                createResult.Meal = Mapper.Map<MealForListingDTO>(createdMeal);
                createResult.Meal.SetTotalMacros(createdMeal.Products);
            }

            return createResult;
        }

        public UpdateMealResultDTO UpdateMeal(UpdateMealDTO mealDTO)
        {
            var updateResult = new UpdateMealResultDTO
            {
                Updated = false,
                Meal = null
            };

            var meal = _mealRepository.GetMealById(mealDTO.Id);
            var products = new List<ProductInMeal>();
            foreach (var product in mealDTO.Products)
            {
                if (product.Id != null && _productInMealRepository.ProductInMealExists((int)product.Id))
                {
                    var productInMealEntity = _productInMealRepository.GetProductInMeal((int)product.Id);
                    products.Add(productInMealEntity);
                }
                else
                {
                    var productEntity = _foodProductRepository.GetFoodProductByID(product.ProductId);
                    products.Add(new ProductInMeal { AmountInGrams = product.AmountInGrams, Product = productEntity, ProductId = productEntity.Id, Meal = meal, MealId = meal.Id });
                }
            }

            meal.Date = mealDTO.Date;
            meal.Name = mealDTO.Name;
            meal.Products = products;

            updateResult.Meal = Mapper.Map<MealForListingDTO>(meal);

            _mealRepository.UpdateMeal(meal);
            updateResult.Updated = _mealRepository.Save();

            return updateResult;
        }

        public DeleteMealResultDTO DeleteMeal(int id)
        {
            var meal = _mealRepository.GetMealById(id);
            if (meal == null)
                return null;

            var deleteMealResult = new DeleteMealResultDTO
            {
                Deleted = false,
                Meal = Mapper.Map<MealForListingDTO>(meal)
            };

            _mealRepository.DeleteMeal(meal);
            deleteMealResult.Deleted = _mealRepository.Save();

            return deleteMealResult;
        }
    }
}