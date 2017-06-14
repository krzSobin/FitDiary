using Dapper;
using FitDiary.Contracts.DTOs.Training;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FitDiary.SecuredApi.Services.Training
{
    public class MusclesService
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["FitDiarySecuredApiContext"].ConnectionString; //TODO DI

        public async Task<IEnumerable<MuscleDTO>> GetMusclesAsync()
        {
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT m.Id, m.Name
                            FROM [Muscles] m";
                return await con.QueryAsync<MuscleDTO>(sql);
            }
        }

        public async Task<MuscleDTO> GetMuscleAsync(int id)
        {
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT m.Id, m.Name
                            FROM [Muscles] m
                            WHERE m.Id = @Id";

                return await con.QueryFirstOrDefaultAsync<MuscleDTO>(sql, new { Id = id });
            }
        }
    }
}