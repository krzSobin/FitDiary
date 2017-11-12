using Dapper;
using FitDiary.Contracts.DTOs.Training;
using FitDiary.SecuredApi.Models.Training;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace FitDiary.SecuredApi.Services.Training
{
    public class ExcercisesService
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["FitDiarySecuredApiContext"].ConnectionString; //TODO DI

        public async Task<IEnumerable<ExcerciseForListingDTO>> GetExcercisesAsync()
        {
            var excerciseDTOs = new List<ExcerciseForListingDTO>();

            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                var sqlForExcercises = @"SELECT e.Id, e.Name
                                        FROM [Excercises] e";
                var excercises = await con.QueryAsync(sqlForExcercises);

                foreach (var excercise in excercises)
                {
                    excerciseDTOs.Add(new ExcerciseForListingDTO { Id = excercise.Id, Name = excercise.Name });
                }

                return excerciseDTOs;
            }
        }

        public async Task<IEnumerable<ExcerciseForListingDTO>> GetExcercisesByMainMuscleAsync(int MuscleId, ExcerciseQueryParams queryParams)
        {
            var sqlForExcercises = @"SELECT e.Id, e.Name AS Name
                                        FROM [Excercises] e
                                        JOIN [MuscleInExcercises] mie on mie.ExcerciseId = e.Id
                                        JOIN [Muscles] m on m.Id = mie.MuscleId
							            WHERE mie.IsMainMuscle = 1 AND m.Id = @Id";

            var sb = new StringBuilder(sqlForExcercises);

            if (!string.IsNullOrWhiteSpace(queryParams.Name))
            {
                sb.Append(" AND e.Name LIKE CONCAT('%', @ExName, '%')");
            }
            sb.Append(" ORDER BY Name");

            if (queryParams.SortOrder == SortOrder.Descending)
            {
                sb.Append(" DESC");
            }

            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                return await con.QueryAsync<ExcerciseForListingDTO>(sb.ToString(), new { Id = MuscleId, ExName = queryParams.Name });
            }
        }
    }
}