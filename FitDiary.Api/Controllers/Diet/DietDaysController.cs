using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using FitDiary.Api.DAL;
using FitDiary.Contracts.DTOs.Diet;
using System.Web.Http.Cors;

namespace FitDiary.Api.Controllers.Diet
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/days")]
    public class DietDaysController : ApiController
    {
        private FitDiaryApiContext db = new FitDiaryApiContext();

        // GET: api/DietDays
        [HttpGet]
        [Route("")]
        public IQueryable<DietDayDTO> GetMeals()
        {
            var macrosList = new List<double>();
            var meals = db.Meals
                .GroupBy(m => DbFunctions.TruncateTime(m.Date))
                .Select(d =>
                new DietDayDTO
                {
                    Date = (DateTime)DbFunctions.TruncateTime(d.FirstOrDefault().Date),
                    Macros = new List<double>
                            {
                                d.Sum(s => s.TotalProtein),
                                d.Sum(s => s.TotalFat),
                                d.Sum(s => s.TotalCarb)
                            },
                    MealsCount = d.Count(),
                    TotalKCal = d.Sum(s => s.TotalKcal),
                    RealizationPercent = 0.0 //TODO
                })
                .OrderBy(m => m.Date);

            return meals;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MealExists(int id)
        {
            return db.Meals.Count(e => e.Id == id) > 0;
        }
    }
}