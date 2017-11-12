using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using System.Collections.Generic;
using FitDiary.Contracts.DTOs.Diet;
using System;
using FitDiary.SecuredApi.Models;
using System.Web;
using Microsoft.AspNet.Identity;
using FitDiary.SecuredApi.Diet.BLL.Meals;
using FitDiary.SecuredApi.Diet.DAL.Meals;
using FitDiary.Contracts.DTOs.Diet.Meals;
using FitDiary.SecuredApi.Diet.DAL.ProductsInMeal;
using FitDiary.SecuredApi.Diet.DAL.FoodProducts;

namespace FitDiary.SecuredApi.Controllers.Diet
{
    //[Authorize]
    [RoutePrefix("api/meals")]
    public class MealsController : ApiController
    {
        private readonly MealsService _mealsService;

        public MealsController()
        {
            var context = new ApplicationDbContext();

            var mealRepository = new MealRepository(context);
            var foodProductRepository = new FoodProductRepository(context);
            var productInMealRepository = new ProductInMealRepository(context);

            _mealsService = new MealsService(mealRepository, foodProductRepository, productInMealRepository);
        }

        public MealsController(IMealRepository mealRepository, IFoodProductRepository foodProductRepository, IProductInMealRepository productInMealRepository)
        {
            _mealsService = new MealsService(mealRepository, foodProductRepository, productInMealRepository);
        }

        // GET: api/Meals
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IEnumerable<MealsDailyDTO>))]
        public IHttpActionResult GetMeals()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId<int>();
            var meals = _mealsService.GetMeals(userId);

            return Ok(meals);
        }

        // GET: api/Meals/5
        [HttpGet]
        [Route("{id:int}", Name = "GetMealById")]
        [ResponseType(typeof(MealForListingDTO))]
        public IHttpActionResult GetMeal(int id)
        {
            var result = _mealsService.GetMealById(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("{date:datetime}", Name = "GetMealByDate")]
        [ResponseType(typeof(IEnumerable<MealForListingDTO>))]
        public IHttpActionResult GetMeals(DateTime date)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId<int>();
            var result = _mealsService.GetMealsByDate(date, userId);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // PUT: api/Meals/5
        [HttpPut]
        [Route("id:int")]
        [ResponseType(typeof(UpdateMealResultDTO))]
        public IHttpActionResult PutMeal(int id, UpdateMealDTO meal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != meal.Id)
            {
                return BadRequest();
            }

            var updateResult = _mealsService.UpdateMeal(meal);

            if (updateResult.Updated)
                return Ok(updateResult);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Meals
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(AddMealResultDTO))]
        public IHttpActionResult PostMeal(MealInsertOrUpdateDTO meal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            meal.UserId = HttpContext.Current.User.Identity.GetUserId<int>();
            var result = _mealsService.AddMeal(meal);
            if (!result.Added)
            {
                return BadRequest("Adding meal error. Try again.");
            }
            
            return CreatedAtRoute("GetMealById", new { id = result.Meal.Id }, result.Meal);
        }

        // DELETE: api/Meals/5
        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof(DeleteMealResultDTO))]
        public IHttpActionResult DeleteMeal(int id)
        {
            var meal = _mealsService.GetMealById(id);
            if (meal == null)
            {
                return NotFound();
            }

            var result = _mealsService.DeleteMeal(id);

            return Ok(result);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                
            }
            base.Dispose(disposing);
        }
    }
}