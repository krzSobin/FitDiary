using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using FitDiary.Api.DAL;
using FitDiary.Api.Models;
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
        public IEnumerable<DietDayDTO> GetMeals()
        {
            var meals2 = db.Meals
                .GroupBy(m => m.Date)
                .Select(d =>
                new DietDayDTO
                {
                    Date = d.FirstOrDefault().Date,
                    Macros = new List<double>()
                            {
                                d.Sum(s => s.TotalProtein),
                                d.Sum(s => s.TotalFat),
                                d.Sum(s => s.TotalCarb)
                            },
                    MealsCount = d.Count(),
                    TotalKCal = d.Sum(s => s.TotalKcal),
                    RealizationPercent = 0.0
                })
                .OrderBy(m => m.Date)
                .ToList();

            //var meals = db.Meals.GroupBy(m => m.Date);
            //var macros = new double[3]
            //{
            //    meals.Sum(s => s.TotalProtein),
            //    meals.Sum(s => s.TotalFat),
            //    meals.Sum(s => s.TotalCarb)
            //};
            //var day = new DietDayDTO
            //{
            //    MealsCount = meals.Count(),
            //    Macros = macros,
            //    TotalKCal = meals.Sum(s => s.TotalKcal),
            //    RealizationPercent = 0
            //};

            return meals2;
        }

        // GET: api/DietDays/5
        [ResponseType(typeof(Meal))]
        public async Task<IHttpActionResult> GetMeal(int id)
        {
            Meal meal = await db.Meals.FindAsync(id);
            if (meal == null)
            {
                return NotFound();
            }

            return Ok(meal);
        }

        // PUT: api/DietDays/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMeal(int id, Meal meal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != meal.Id)
            {
                return BadRequest();
            }

            db.Entry(meal).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MealExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/DietDays
        [ResponseType(typeof(Meal))]
        public async Task<IHttpActionResult> PostMeal(Meal meal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Meals.Add(meal);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = meal.Id }, meal);
        }

        // DELETE: api/DietDays/5
        [ResponseType(typeof(Meal))]
        public async Task<IHttpActionResult> DeleteMeal(int id)
        {
            Meal meal = await db.Meals.FindAsync(id);
            if (meal == null)
            {
                return NotFound();
            }

            db.Meals.Remove(meal);
            await db.SaveChangesAsync();

            return Ok(meal);
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