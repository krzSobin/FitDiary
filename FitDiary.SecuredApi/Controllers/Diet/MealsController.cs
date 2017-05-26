using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Collections.Generic;
using FitDiary.Contracts.DTOs.Diet;
using System;
using System.Web.Http.Cors;
using FitDiary.SecuredApi.Models.Diet;
using FitDiary.SecuredApi.Models;
using FitDiary.SecuredApi.Services.Diet;

namespace FitDiary.SecuredApi.Controllers.Diet
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/meals")]
    public class MealsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly MealsService _mealsSrv = new MealsService();

        // GET: api/Meals
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<MealDTO>> GetMealsAsync()
        {
            var meals = await _mealsSrv.GetMealsAsync();

            return meals;
        }

        // GET: api/Meals
        [HttpGet]
        [Route("zmienic")]
        public async Task<IEnumerable<DietDayDTO>> GetMealsPerDaysAsync()
        {
            var result = await _mealsSrv.GetMealsInDaysRangeAsync(new DateTime(2009, 12, 12), DateTime.UtcNow);
            return result;
        }


        // GET: api/Meals/5
        [HttpGet]
        [Route("{id:int}", Name = "GetMealById")]
        [ResponseType(typeof(MealDTO))]
        public async Task<IHttpActionResult> GetMealAsync(int id)
        {
            var result = await _mealsSrv.GetMealsByDAyAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("{date:datetime}", Name = "GetMealByDate")]
        [ResponseType(typeof(IEnumerable<Meal>))]
        public async Task<IEnumerable<MealDTO>> GetMealAsync(DateTime date)
        {
            var result = await _mealsSrv.GetMealsByDAyAsync(date);

            return result;
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