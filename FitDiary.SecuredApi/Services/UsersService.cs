using Dapper;
using FitDiary.Contracts.DTOs;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FitDiary.SecuredApi.Services
{
    public class UsersService
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["FitDiarySecuredApiContext"].ConnectionString;

        public async Task<UserDTO> GetUserData(string id)
        {
            var sql = @"SELECT users.UserName, users.email, users.age, users.Weight, users.HeightInCm
                                        FROM [AspNetUsers] users
                                        WHERE users.Id = @Id";

            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                var result = await con.QueryFirstOrDefaultAsync<UserDTO>(sql, new { Id = id });

                return result;
            }
        }
    }
}