using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using FitDiary.Api.Models;
using FitDiary.Api.DAL;
using FitDiary.Contracts.DTOs;
using System.Collections.Generic;

namespace FitDiary.Api.Controllers
{
    [RoutePrefix("api/foodProducts")]
    public class FoodProductsController : ApiController
    {
        private FitDiaryApiContext db = new FitDiaryApiContext();

        // GET: api/FoodProducts
        [HttpGet]
        [Route("")]
        public IEnumerable<FoodProductDTO> GetFoodProducts()
        {
            var products = db.FoodProducts.Select(product =>
            new FoodProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category.Name,
                CarboPer100g = product.CarboPer100g,
                ProteinsPer100g = product.ProteinsPer100g,
                FatsPer100g = product.FatsPer100g,
                SugarPer100g = product.SugarPer100g
            });
            var a = products.ToList<FoodProductDTO>();
            return a;
        }

        [HttpGet]
        [Route("{id:int}")]
        [ResponseType(typeof(FoodProduct))]
        public async Task<IHttpActionResult> GetFoodProduct(int id)
        {
            var product = await db.FoodProducts.Select(p =>
            new FoodProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category.Name,
                CarboPer100g = p.CarboPer100g,
                ProteinsPer100g = p.ProteinsPer100g,
                FatsPer100g = p.FatsPer100g,
                SugarPer100g = p.SugarPer100g
            }).SingleOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/FoodProducts/5
        [HttpPut]
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFoodProduct(int id, FoodProduct foodProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != foodProduct.Id)
            {
                return BadRequest();
            }

            db.Entry(foodProduct).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodProductExists(id))
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

        // POST: api/FoodProducts
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(FoodProduct))]
        public async Task<IHttpActionResult> PostFoodProduct(FoodProduct foodProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FoodProducts.Add(foodProduct);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = foodProduct.Id }, foodProduct);
        }

        // DELETE: api/FoodProducts/5
        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof(FoodProduct))]
        public async Task<IHttpActionResult> DeleteFoodProduct(int id)
        {
            FoodProduct foodProduct = await db.FoodProducts.FindAsync(id);
            if (foodProduct == null)
            {
                return NotFound();
            }

            db.FoodProducts.Remove(foodProduct);
            await db.SaveChangesAsync();

            return Ok(foodProduct);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FoodProductExists(int id)
        {
            return db.FoodProducts.Count(e => e.Id == id) > 0;
        }
    }
}