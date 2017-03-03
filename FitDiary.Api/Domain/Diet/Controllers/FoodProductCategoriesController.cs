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
using FitDiary.Api.Diet.Models;

namespace FitDiary.Api.Diet.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/foodCategories")]
    public class FoodProductCategoriesController : ApiController
    {
        private FitDiaryApiContext db = new FitDiaryApiContext();

        // GET: api/foodCategories
        [HttpGet]
        [Route("")]
        public IEnumerable<FoodProductCategoryDTO> GetFoodProductCategories()
        {
            var categories = db.FoodProductCategories.Select(c =>
            new FoodProductCategoryDTO
            {
                Id = c.Id,
                Name = c.Name
            });

            return categories.ToList<FoodProductCategoryDTO>();
        }

        // GET: api/FoodProductCategories/5
        [HttpGet]
        [Route("{id:int}", Name = "GetFoodCategoryById")]
        [ResponseType(typeof(FoodProductCategory))]
        public async Task<IHttpActionResult> GetFoodProductCategory(int id)
        {
            FoodProductCategory foodProductCategory = await db.FoodProductCategories.FindAsync(id);
            if (foodProductCategory == null)
            {
                return NotFound();
            }

            return Ok(foodProductCategory);
        }

        // PUT: api/FoodProductCategories/5
        [HttpPut]
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFoodProductCategory(int id, FoodProductCategory foodProductCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != foodProductCategory.Id)
            {
                return BadRequest();
            }

            db.Entry(foodProductCategory).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodProductCategoryExists(id))
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

        // POST: api/FoodProductCategories
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(FoodProductCategory))]
        public async Task<IHttpActionResult> PostFoodProductCategory(FoodProductCategory foodProductCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FoodProductCategories.Add(foodProductCategory);
            await db.SaveChangesAsync();

            return CreatedAtRoute("GetFoodCategoryById", new { id = foodProductCategory.Id }, foodProductCategory);
        }

        // DELETE: api/FoodProductCategories/5
        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof(FoodProductCategory))]
        public async Task<IHttpActionResult> DeleteFoodProductCategory(int id)
        {
            FoodProductCategory foodProductCategory = await db.FoodProductCategories.FindAsync(id);
            if (foodProductCategory == null)
            {
                return NotFound();
            }

            db.FoodProductCategories.Remove(foodProductCategory);
            await db.SaveChangesAsync();

            return Ok(foodProductCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FoodProductCategoryExists(int id)
        {
            return db.FoodProductCategories.Count(e => e.Id == id) > 0;
        }
    }
}