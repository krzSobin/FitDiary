using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using FitDiary.Api.DAL;
using System.Web.Http.Cors;
using System.Collections.Generic;
using FitDiary.Contracts.DTOs.Diet;
using System;
using FitDiary.Api.Diet.Models;

namespace FitDiary.Api.Diet.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/meals")]
    public class MealsController : ApiController
    {
        private FitDiaryApiContext db = new FitDiaryApiContext();

        // GET: api/Meals
        [HttpGet]
        [Route("")]
        public IEnumerable<MealDTO> GetMeals()
        {
            var meals = db.Meals.Select(m =>
            new MealDTO
            {
                Id = m.Id,
                Date = m.Date,
                TotalKcal = m.TotalKcal,
                TotalProtein = m.TotalProtein,
                TotalFat = m.TotalFat,
                TotalCarb = m.TotalCarb,
                TotalSugar = m.TotalSugar
            });
            var mealsList = meals.ToList<MealDTO>();

            return mealsList;
        }

        // GET: api/Meals
        [HttpGet]
        [Route("test")]
        public IEnumerable<Meal> GetMealsTest()
        {
            return db.Meals.Include("Products.Product");
        }

        // GET: api/Meals/5
        [HttpGet]
        [Route("{id:int}", Name = "GetMealById")]
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

        [HttpGet]
        [Route("{date:datetime}", Name = "GetMealByDate")]
        [ResponseType(typeof(IEnumerable<Meal>))]
        public IEnumerable<MealDTO> GetMeal(DateTime date)
        {
            var meals = db.Meals.Select(m =>
            new MealDTO
            {
                Id = m.Id,
                Date = m.Date,
                TotalKcal = m.TotalKcal,
                TotalProtein = m.TotalProtein,
                TotalFat = m.TotalFat,
                TotalCarb = m.TotalCarb,
                TotalSugar = m.TotalSugar
            })
            .Where(m => DbFunctions.TruncateTime(m.Date) == date);
            var mealsList = meals.ToList<MealDTO>();

            return mealsList;
        }

        // PUT: api/Meals/5
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

        // POST: api/Meals
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(Meal))]
        public async Task<IHttpActionResult> PostMeal(Meal meal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Meals.Add(meal);

            foreach (ProductInMeal productsInMeal in meal.Products)
            {
                db.ProductsInMeal.Add(productsInMeal);
            }
            await db.SaveChangesAsync();

            return CreatedAtRoute("GetMealById", new { id = meal.Id }, meal);
        }

        // DELETE: api/Meals/5
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