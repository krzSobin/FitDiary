using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using FitDiary.Api.Models;
using System.Collections.Generic;
using System.Web.Http.Cors;
using FitDiary.Contracts.DTOs.Diet;
using FitDiary.Api.Services;
using FitDiary.Api.Models.QueryModels;

namespace FitDiary.Api.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/foodProducts")]
    public class FoodProductsController : ApiController
    {
        private readonly IFoodProductService _foodProdSrv;

        public FoodProductsController(IFoodProductService foodProductService)
        {
            _foodProdSrv = foodProductService;
        }

        // GET: api/FoodProducts
        [HttpGet]
        [Route("")]
        public IEnumerable<FoodProductDTO> GetFoodProducts([FromUri]FoodProductQuery query)
        {
            if (query == null)
            {
                query = new FoodProductQuery();
            }
            return _foodProdSrv.GetFoodProducts(query);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetFoodProductById")]
        [ResponseType(typeof(FoodProduct))]
        public async Task<IHttpActionResult> GetFoodProductAsync(int id)
        {
            var product = await _foodProdSrv.GetFoodProductAsync(id).ConfigureAwait(false);

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
        public async Task<IHttpActionResult> PutFoodProductAsync(int id, FoodProduct foodProduct)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != foodProduct.Id)
                return BadRequest();

            try
            {
                await _foodProdSrv.PutFoodProductAsync(id, foodProduct);
            }
            catch (HttpResponseException e)
            {
                if (e.Response.StatusCode == HttpStatusCode.NotFound)
                    return NotFound();
                else
                    throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/FoodProducts
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(FoodProduct))]
        public async Task<IHttpActionResult> PostFoodProductAsync(FoodProduct foodProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var prod = await _foodProdSrv.PostFoodProductAsync(foodProduct).ConfigureAwait(false);

            return CreatedAtRoute("GetFoodProductById", new { id = prod.Id }, prod);
        }

        // DELETE: api/FoodProducts/5
        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof(FoodProduct))]
        public async Task<IHttpActionResult> DeleteFoodProductAsync(int id)
        {
            try
            {
                var product = await _foodProdSrv.DeleteFoodProductAsync(id).ConfigureAwait(false);

                return Ok(product);
            }
            catch (HttpResponseException e)
            {
                if (e.Response.StatusCode != HttpStatusCode.NotFound)
                    throw;
                return NotFound();
            }
        }
    }
}