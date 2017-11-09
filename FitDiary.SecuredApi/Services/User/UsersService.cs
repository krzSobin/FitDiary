using Dapper;
using FitDiary.Contracts.DTOs.User;
using FitDiary.SecuredApi.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FitDiary.SecuredApi.Services.User
{
    public class UsersService
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["FitDiarySecuredApiContext"].ConnectionString;
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public async Task<UserDTO> GetUserData(int id)
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

        public async Task<BodyGoalsDTO> GetUserBodyGoals(int id)
        {
            var sql = @"SELECT b.Id, b.StartDate, b.EndDate, b.WeightInKg, b.ChestInCm, b.WaistInCm, b.Status
                                        FROM [BodyGoals] b";

            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                var result = await con.QueryFirstOrDefaultAsync<BodyGoalsDTO>(sql, new { Id = id });

                return result;
            }
        }

        public async Task<UserFullInfoDTO> GetUserFullInfo(int id)
        {
            var userFullInfo = db.Users.Where(usr => usr.Id == id).Select(u =>
            new UserFullInfoDTO
            {
                UserBaseInfo = new UserDTO
                {
                    Name = u.UserName,
                   // Birthday = u.Birthday,
                    Email = u.Email,
                    //Height = u.HeightInCm
                },
                LastBodyMeasurement = new BodyMeasurementsDTO
                {
                    WeightInKg = u.BodyMeasurements.OrderBy(bm => bm.MeasurementDate).Select(bm => bm.WeightInKg).FirstOrDefault(),
                    BodyFat = u.BodyMeasurements.OrderBy(bm => bm.MeasurementDate).Select(bm => bm.BodyFat).FirstOrDefault(),
                    ChestInCm = u.BodyMeasurements.OrderBy(bm => bm.MeasurementDate).Select(bm => bm.ChestInCm).FirstOrDefault(),
                    WaistInCm = u.BodyMeasurements.OrderBy(bm => bm.MeasurementDate).Select(bm => bm.WaistInCm).FirstOrDefault(),
                    MeasurementDate = u.BodyMeasurements.OrderBy(bm => bm.MeasurementDate).Select(bm => bm.MeasurementDate).FirstOrDefault()
                }
            }).FirstOrDefault();

            return userFullInfo;
        }
    }
}