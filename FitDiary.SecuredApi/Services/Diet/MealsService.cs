using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using FitDiary.Contracts.DTOs.Diet;
using FitDiary.SecuredApi.Models.Diet;
using Dapper.Contrib.Extensions;
using System.Web.Http;

namespace FitDiary.SecuredApi.Services.Diet
{
    public class MealsService
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["FitDiarySecuredApiContext"].ConnectionString; //TODO DI

        public async Task<IEnumerable<MealForListingDTO>> GetMealsAsync(string userId)
        {
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                var result = await con.QueryAsync<MealForListingDTO>(@"SELECT m.id, m.date, SUM(pim.amountInGrams*fp.kCalPer100g/100) AS TotalKCal,
                                                                    SUM(pim.amountInGrams*fp.proteinsPer100g/100) AS TotalProtein, SUM(pim.amountInGrams*fp.fatsPer100g/100) AS TotalFat,
                                                                    SUM(pim.amountInGrams*fp.CarboPer100g/100) AS TotalCarb, SUM(pim.amountInGrams*fp.sugarPer100g/100) AS TotalSugar
                                                            FROM [Meals] m
                                                            JOIN [ProductInMeals] pim on pim.mealId = m.Id
                                                            JOIN [FoodProducts] fp on fp.id = pim.productId
                                                            WHERE m.userId = @UserId
															group by m.id, m.date", new { UserId = userId });

                return result;
            }
        }

        public async Task<IEnumerable<MealForListingDTO>> GetMealsByDAyAsync(DateTime mealsDay, string userId)
        {
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT m.id, m.date, SUM(pim.amountInGrams*fp.kCalPer100g/100) AS TotalKCal,
                                    SUM(pim.amountInGrams*fp.proteinsPer100g/100) AS TotalProtein, SUM(pim.amountInGrams*fp.fatsPer100g/100) AS TotalFat,
                                    SUM(pim.amountInGrams*fp.CarboPer100g/100) AS TotalCarb, SUM(pim.amountInGrams*fp.sugarPer100g/100) AS TotalSugar
                            FROM [Meals] m
                            JOIN [ProductInMeals] pim on pim.mealId = m.Id
                            JOIN [FoodProducts] fp on fp.id = pim.productId
                            WHERE datediff(day, m.date, @MealsDay) = 0 AND m.userId = @UserId
                            GROUP BY m.id, m.date";

                var result = await con.QueryAsync<MealForListingDTO>(sql, new { MealsDay = mealsDay, UserId = userId });

                return result;
            }
        }

        public async Task<IEnumerable<DietDayDTO>> GetMealsInDaysRangeAsync(DateTime mealDateRangeStart, DateTime mealDateRangeEnd) //TODO walidacja parametrow
        {
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT CAST(meals.Date AS DATE) as Date, SUM(1) as MealsCount, SUM(meals.TotalKCal) as TotalKCal, SUM(meals.TotalProtein) AS TotalProteins, SUM(meals.TotalCarb) AS TotalCarbs, SUM(meals.TotalSugar) AS TotalSugar, SUM(meals.TotalFat) AS TotalFats, SUM(meals.TotalKCal)/2500*100 as RealizationPercent
                            FROM 
								(SELECT m.id, m.date AS date, SUM(pim.amountInGrams*fp.kCalPer100g/100) AS TotalKCal,
                                    SUM(pim.amountInGrams*fp.proteinsPer100g/100) AS TotalProtein, SUM(pim.amountInGrams*fp.fatsPer100g/100) AS TotalFat,
                                    SUM(pim.amountInGrams*fp.CarboPer100g/100) AS TotalCarb, SUM(pim.amountInGrams*fp.sugarPer100g/100) AS TotalSugar
								FROM [Meals] m
								JOIN [ProductInMeals] pim on pim.mealId = m.Id
								JOIN [FoodProducts] fp on fp.id = pim.productId
								GROUP BY m.id, m.date) AS meals
							WHERE CAST(meals.Date AS DATE) >= '2016-02-10' AND CAST(meals.Date AS DATE) <= '2018-02-20'
                            GROUP BY CAST(meals.Date AS DATE)
							order by CAST(meals.Date AS DATE)";

                var result = await con.QueryAsync<DietDayDTO>(sql, new { MealDateRangeStart = mealDateRangeStart.Date, MealDateRangeEnd = mealDateRangeEnd.Date });

                return result;
            }
        }

        public async Task<MealForListingDTO> GetMealByIdAsync(int id)
        {
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT m.id, m.date, SUM(pim.amountInGrams*fp.kCalPer100g/100) AS TotalKCal,
                                    SUM(pim.amountInGrams*fp.proteinsPer100g/100) AS TotalProtein, SUM(pim.amountInGrams*fp.fatsPer100g/100) AS TotalFat,
                                    SUM(pim.amountInGrams*fp.CarboPer100g/100) AS TotalCarb, SUM(pim.amountInGrams*fp.sugarPer100g/100) AS TotalSugar
                            FROM [Meals] m
                            JOIN [ProductInMeals] pim on pim.mealId = m.Id
                            JOIN [FoodProducts] fp on fp.id = pim.productId
                            WHERE m.id = @Id
                            GROUP BY m.id, m.date";

                var result = await con.QueryFirstOrDefaultAsync<MealForListingDTO>(sql, new { Id = id });

                return result;
            }
        }

        public async Task<int> AddMeal(MealInsertOrUpdateDTO meal)
        {
            int mealId;
            var sqlForMeals = @"INSERT INTO [Meals] ([Name], [Date], [UserId])
                                VALUES(@Name, @Date, @UserId);
                                SELECT SCOPE_IDENTITY();";

            var sqlForProducts = @"INSERT INTO [ProductInMeals] ([AmountInGrams], [ProductId], [MealId])
                                VALUES(@AmountInGrams, @ProductId, @MealId)";

            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                using (var tran = con.BeginTransaction())
                {
                    try
                    {

                        mealId = await con.QueryFirstOrDefaultAsync<int>(sqlForMeals, new { Name = meal.Name, Date = meal.Date, UserId = meal.UserId }, tran);
                        foreach (var productInMeal in meal.Products)
                        {
                            await con.ExecuteAsync(sqlForProducts, new { AmountInGrams = productInMeal.AmountInGrams, ProductId = productInMeal.ProductId, MealId = mealId }, tran);
                        }

                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }

                return mealId;
            }
        }

        public async Task<int> UpdateMeal(MealInsertOrUpdateDTO meal)
        {
            int mealId;
            var sqlForMeals = @"UPDATE [Meals]
                                SET ([Name], [Date], [UserId])
                                VALUES(@Name, @Date, @UserId);
                                SELECT SCOPE_IDENTITY();";

            var sqlForProducts = @"INSERT INTO [ProductInMeals] ([AmountInGrams], [ProductId], [MealId])
                                VALUES(@AmountInGrams, @ProductId, @MealId)";

            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                using (var tran = con.BeginTransaction())
                {
                    try
                    {

                        mealId = await con.QueryFirstOrDefaultAsync<int>(sqlForMeals, new { Name = meal.Name, Date = meal.Date, UserId = meal.UserId }, tran);
                        foreach (var productInMeal in meal.Products)
                        {
                            await con.ExecuteAsync(sqlForProducts, new { AmountInGrams = productInMeal.AmountInGrams, ProductId = productInMeal.ProductId, MealId = mealId }, tran);
                        }

                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }

                return mealId;
            }
        }

        public async Task<int> DeleteMeal(int id)
        {
            var sqlDeleteMeal = @"DELETE FROM [Meals]
                        WHERE Id = @Id";

            var sqlDeleteProductsInMeal = @"DELETE FROM [ProductInMeals]
                                            WHERE MealId = @Id";
            int result;

            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                using (var tran = con.BeginTransaction())
                {
                    try
                    {

                        await con.ExecuteAsync(sqlDeleteProductsInMeal, new { Id = id }, tran);

                        result = await con.ExecuteAsync(sqlDeleteMeal, new { Id = id }, tran);

                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }

            return result;
        }
    }
}