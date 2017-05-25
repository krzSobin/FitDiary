using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using FitDiary.Contracts.DTOs.Diet;

namespace FitDiary.SecuredApi.Services.Diet
{
    public class MealsService
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["FitDiarySecuredApiContext"].ConnectionString; //TODO DI
        
        public async Task<IEnumerable<MealDTO>> GetMealsAsync()
        {
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                var result = await con.QueryAsync<MealDTO>(@"SELECT m.id, m.date, m.totalKcal, m.TotalProtein, m.totalFat, m.totalCarb, m.totalSugar
                                        FROM [Meals] m
                                        JOIN [FoodProductCategories] cat on fp.categoryId = cat.id");

                return result;
            }
        }

        public async Task<IEnumerable<MealDTO>> GetMealsByDAyAsync(DateTime mealsDay)
        {
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT m.id, m.date, m.totalKcal, m.TotalProtein, m.totalFat, m.totalCarb, m.totalSugar
                                        FROM [Meals] m
                                        WHERE datediff(day, m.date, @MealsDay) = 0";

                var result = await con.QueryAsync<MealDTO>(sql, new { MealsDay = mealsDay });

                return result;
            }
        }

        public async Task<IEnumerable<DietDayDTO>> GetMealsInDaysRangeAsync(DateTime mealDateRangeStart, DateTime mealDateRangeEnd) //TODO walidacja parametrow
        {
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT CAST(Date AS DATE) as Date, SUM(1) as MealsCount, SUM(TotalKcal) as TotalKCal, SUM(TotalProtein) AS TotalProteins, SUM(TotalCarb) AS TotalCarbs, SUM(TotalSugar) AS TotalSugar, SUM(TotalFat) AS TotalFats, SUM(TotalKcal)/2500*100 as RealizationPercent
                            FROM [Meals]
							WHERE CAST(Date AS DATE) >= @MealDateRangeStart AND CAST(Date AS DATE) <= @MealDateRangeEnd
                            GROUP BY CAST(Date AS DATE)
							order by date";

                var result = await con.QueryAsync<DietDayDTO>(sql, new { MealDateRangeStart = mealDateRangeStart.Date, MealDateRangeEnd = mealDateRangeEnd.Date });

                return result;
            }
        }

        public async Task<MealDTO> GetMealsByDAyAsync(int id)
        {
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT m.id, m.date, m.totalKcal, m.TotalProtein, m.totalFat, m.totalCarb, m.totalSugar
                                        FROM [Meals] m
                                        WHERE m.id = @Id";

                var result = await con.QueryFirstOrDefaultAsync<MealDTO>(sql, new { Id = id });

                return result;
            }
        }
    }
}