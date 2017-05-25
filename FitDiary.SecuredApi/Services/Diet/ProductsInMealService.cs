using Dapper;
using FitDiary.Contracts.DTOs.Diet;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FitDiary.SecuredApi.Services.Diet
{
    public class ProductsInMealService
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["FitDiarySecuredApiContext"].ConnectionString;

        public async Task<IEnumerable<ProductInMealDTO>> GetProductsInMealAsync(int mealId)
        {
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT
                                    pm.id, pm.mailId, pm.amountInGrams, fp.name, fp.kcalPer100g * pm.amountInGrams,
                                    fp.proteinsPer100g * pm.amountInGrams, fp.fatsPer100g * pm.amountInGrams, 
                                    p.carbsPer100g * pm.amountInGrams, fp.sugarPer100g * pm.amountInGrams
                            FROM [ProductsInMeal] pm
                            JOIN [FoodProducts] fp on pm.productId = fp.id
                            WHERE pm.mealId = @mealId";

                var result = await con.QueryAsync<ProductInMealDTO>(sql, new { mealId = mealId });

                return result.ToList();
            }
        }
    }
}