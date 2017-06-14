using System.Collections.Generic;
using System.Threading.Tasks;
using FitDiary.Contracts.DTOs.Training;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using System.Configuration;

namespace FitDiary.SecuredApi.Services.Training
{
    public class WorkoutsService
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["FitDiarySecuredApiContext"].ConnectionString; //TODO DI

        internal async Task<IEnumerable<WorkoutDTO>> GetWorkoutsAsync()
        {
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT w.Id, w.Name, w.Date
                            FROM [Workouts] w";

                return await con.QueryAsync<WorkoutDTO>(sql);
            }
        }

        internal async Task<WorkoutDTO> GetWorkoutAsync(int id)
        {
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT w.Id, w.Name, w.Date
                            FROM [Workouts] w
                            WHERE w.Id = @Id";

                return await con.QueryFirstOrDefaultAsync<WorkoutDTO>(sql, new { Id = id });
            }
        }
    }
}