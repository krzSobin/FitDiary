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
using System.Web.Http.Cors;

namespace FitDiary.Api.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/productsInMeals")]
    public class ProductsInMealController : ApiController
    {
        private FitDiaryApiContext db = new FitDiaryApiContext();

        // GET: api/ProductsInMeal
        [HttpGet]
        [Route("meal/{mealId:int}")]
        public IQueryable<ProductInMeal> GetProductsInMeal(int mealId)
        {
            return db.ProductsInMeal.Include(p => p.Product).Where(p => p.MealId == mealId);
        }

        // GET: api/ProductsInMeal/5
        [HttpGet]
        [Route("{id:int}")]
        [ResponseType(typeof(ProductInMeal))]
        public async Task<IHttpActionResult> GetProductInMeal(int id)
        {
            ProductInMeal productInMeal = await db.ProductsInMeal.FindAsync(id);
            if (productInMeal == null)
            {
                return NotFound();
            }

            return Ok(productInMeal);
        }

        // PUT: api/ProductsInMeal/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProductInMeal(int id, ProductInMeal productInMeal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productInMeal.Id)
            {
                return BadRequest();
            }

            db.Entry(productInMeal).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductInMealExists(id))
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

        // POST: api/ProductsInMeal
        [HttpPost]
        [ResponseType(typeof(ProductInMeal))]
        public async Task<IHttpActionResult> PostProductInMeal(ProductInMeal productInMeal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductsInMeal.Add(productInMeal);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = productInMeal.Id }, productInMeal);
        }

        // DELETE: api/ProductsInMeal/5
        [HttpDelete]
        [ResponseType(typeof(ProductInMeal))]
        public async Task<IHttpActionResult> DeleteProductInMeal(int id)
        {
            ProductInMeal productInMeal = await db.ProductsInMeal.FindAsync(id);
            if (productInMeal == null)
            {
                return NotFound();
            }

            db.ProductsInMeal.Remove(productInMeal);
            await db.SaveChangesAsync();

            return Ok(productInMeal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductInMealExists(int id)
        {
            return db.ProductsInMeal.Count(e => e.Id == id) > 0;
        }
    }
}