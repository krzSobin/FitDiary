﻿using FitDiary.Contracts.DTOs.Diet;
using FitDiary.SecuredApi.Models;
using FitDiary.SecuredApi.Models.Diet;
using FitDiary.SecuredApi.Services.Diet;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace FitDiary.SecuredApi.Controllers.Diet
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/foodProducts")]
    public class FoodProductsController : ApiController
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        private readonly FoodProductsService _foodProdSrv = new FoodProductsService();

        // GET: api/FoodProducts
        [Authorize]
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IEnumerable<FoodProduct>))]
        public async Task<IEnumerable<FoodProductDTO>> GetFoodProductsAsync([FromUri] FoodProductQueryParams queryParams)
        {
            if (queryParams != null)
            {
                return await _foodProdSrv.GetFoodProductsAsync(queryParams);
            }

            return await _foodProdSrv.GetFoodProductsAsync();
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetFoodProductById")]
        [ResponseType(typeof(FoodProductDTO))]
        public async Task<IHttpActionResult> GetFoodProduct(int id)
        {
            var product = await _foodProdSrv.GetFoodProductAsync(id);

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

            return CreatedAtRoute("GetFoodProductById", new { id = foodProduct.Id }, foodProduct);
        }

        // DELETE: api/FoodProducts/5
        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof(FoodProductDTO))]
        public async Task<IHttpActionResult> DeleteFoodProduct(int id)
        {
            var foodProduct = await _foodProdSrv.GetFoodProductAsync(id);
            if (foodProduct == null)
            {
                return NotFound();
            }

            if(await _foodProdSrv.DeleteFoodProductAsync(id))
            {
                return Ok(foodProduct);
            }

            return new System.Web.Http.Results.StatusCodeResult(HttpStatusCode.Gone, this);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
            db.Dispose();
        }

        private bool FoodProductExists(int id)
        {
            return db.FoodProducts.Count(e => e.Id == id) > 0;
        }
    }
}
