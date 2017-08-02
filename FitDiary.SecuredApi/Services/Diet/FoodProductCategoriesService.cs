using Dapper;
using FitDiary.Contracts.DTOs.Diet.FoodProductCategories;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FitDiary.SecuredApi.Services.Diet
{
    public class FoodProductCategoriesService
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["FitDiarySecuredApiContext"].ConnectionString; //TODO DI

        public async Task<IEnumerable<CategorySelectDTO>> GetCategoriesAsync()
        {
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                var result = await con.QueryAsync<CategorySelectDTO>(@"SELECT Id, Name
                                        FROM [FoodProductCategories]");

                return result;
            }
        }
    }
}