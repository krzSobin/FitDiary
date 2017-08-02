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
using System.Web;
using Microsoft.AspNet.Identity;

namespace FitDiary.SecuredApi.Controllers.Diet
{
    //[Authorize]
    [RoutePrefix("api/meals")]
    public class MealsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly MealsService _mealsSrv = new MealsService();

        // GET: api/Meals
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IEnumerable<MealForListingDTO>))]
        public async Task<IHttpActionResult> GetMealsAsync()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var meals = await _mealsSrv.GetMealsAsync(userId);

            return Ok(meals);
        }

        // GET: api/Meals
        [HttpGet]
        [Route("zmienic")]
        [ResponseType(typeof(IEnumerable<DietDayDTO>))]
        public async Task<IHttpActionResult> GetMealsPerDaysAsync()//TODO routing + parametry
        {
            var result = await _mealsSrv.GetMealsInDaysRangeAsync(new DateTime(2009, 12, 12), DateTime.UtcNow);
            return Ok(result);
        }


        // GET: api/Meals/5
        [HttpGet]
        [Route("{id:int}", Name = "GetMealById")]
        [ResponseType(typeof(MealForListingDTO))]
        public async Task<IHttpActionResult> GetMealAsync(int id)
        {
            var result = await _mealsSrv.GetMealByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("{date:datetime}", Name = "GetMealByDate")]
        [ResponseType(typeof(IEnumerable<MealForListingDTO>))]
        public async Task<IHttpActionResult> GetMealAsync(DateTime date)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId<int>();
            var result = await _mealsSrv.GetMealsByDAyAsync(date, userId);

            return Ok(result);
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
        public async Task<IHttpActionResult> PostMeal(MealInsertOrUpdateDTO meal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            meal.UserId = HttpContext.Current.User.Identity.GetUserId<int>();
            var result = await _mealsSrv.AddMeal(meal);

            return CreatedAtRoute("GetMealById", new { id = result }, meal);
        }

        // DELETE: api/Meals/5
        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof(Meal))]
        public async Task<IHttpActionResult> DeleteMeal(int id)
        {
            var meal = await _mealsSrv.GetMealByIdAsync(id);
            if (meal == null)
            {
                return NotFound();
            }

            var result = await _mealsSrv.DeleteMeal(id);

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