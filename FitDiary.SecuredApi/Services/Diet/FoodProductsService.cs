using Dapper;
using FitDiary.Contracts.DTOs.Diet;
using FitDiary.SecuredApi.Models.Diet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitDiary.SecuredApi.Services.Diet
{
    public class FoodProductsService
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["FitDiarySecuredApiContext"].ConnectionString;

        #region GetProducts
        public async Task<IEnumerable<FoodProductDTO>> GetFoodProductsAsync()
        {
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                var result = await con.QueryAsync<FoodProductDTO>(@"SELECT fp.id, fp.name as name, fp.proteinsPer100g, fp.fatsPer100g, fp.carboPer100g, fp.sugarPer100g, fp.kcalPer100g, cat.name as category
                                        FROM [FoodProducts] fp
                                        JOIN [FoodProductCategories] cat on fp.categoryId = cat.id");

                return result;
            }
        }
        #endregion
        #region GetProducts(params)
        public async Task<IEnumerable<FoodProductDTO>> GetFoodProductsAsync(FoodProductQueryParams queryParams)//TODO check if null
        {
            string productsQuery;

            if (queryParams != null)
            {
                productsQuery = BuildSqlQuery(queryParams);
            }
            else
            {
                throw new ArgumentNullException(nameof(queryParams));
            }

            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                var result = await con.QueryAsync<FoodProductDTO>(productsQuery, new { CatName = queryParams.Category, ProdName = queryParams.Name, MaxSugar = queryParams.MaxSugar });

                return result;
            }
        }

        private string BuildSqlQuery(FoodProductQueryParams queryParams)
        {
            var sql = @"SELECT fp.id, fp.name as name, fp.proteinsPer100g, fp.fatsPer100g, fp.carboPer100g, fp.sugarPer100g, fp.kcalPer100g, cat.name as category
                                        FROM [FoodProducts] fp
                                        JOIN [FoodProductCategories] cat on fp.categoryId = cat.id
                                        WHERE 1=1";

            var sb = new StringBuilder(sql);

            if (!string.IsNullOrWhiteSpace(queryParams.Category))
            {
                sb.Append(" AND cat.name = @CatName");
            }
            if (!string.IsNullOrWhiteSpace(queryParams.Name))
            {
                sb.Append(" AND fp.name LIKE CONCAT('%', @ProdName, '%')");
            }
            if (queryParams.MaxSugar.HasValue)
            {
                sb.Append(" AND fp.sugarPer100g <= @MaxSugar");
            }

            if (queryParams.SortColumn == SortColumn.SortByName)
            {
                sb.Append(" ORDER BY fp.name");
            }
            else if (queryParams.SortColumn == SortColumn.SortByKCal)
            {
                sb.Append(" ORDER BY fp.kcalPer100g");
            }

            if (queryParams.SortOrder == SortOrder.Descending)
            {
                sb.Append(" DESC");
            }

            //sb.Append(" OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY;"); TODO odkomentowac do paginacji

            return sb.ToString();
        }
        #endregion
        #region GetProduct
        public async Task<FoodProductDTO> GetFoodProductAsync(int id)
        {
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                string sql = @"SELECT fp.id, fp.name, cat.name AS Category, fp.carboPer100g, fp.proteinsPer100g, fp.fatsPer100g, fp.sugarPer100g, fp.kcalPer100g
                                        FROM [FoodProducts] fp
                                        JOIN [FoodProductCategories] cat on fp.categoryId = cat.id
                                        WHERE fp.Id = @Id";

                var result = await con.QueryAsync<FoodProductDTO>(sql, new { Id = id });

                return result.SingleOrDefault();
            }
        }
        #endregion
        #region DeleteProduct
        public async Task<bool> DeleteFoodProductAsync(int id)
        {
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                string sql = @"DELETE FROM [FoodProducts]
                               WHERE Id = @Id";

                return await con.ExecuteAsync(sql, new { Id = id }) > 0;
            }
        }
        #endregion
    }
}