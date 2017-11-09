using AutoMapper;
using FitDiary.Contracts.DTOs.Diet;
using FitDiary.Contracts.DTOs.Diet.FoodProducts;
using FitDiary.SecuredApi.Diet.DAL;
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
    [RoutePrefix("api/foodProducts")]
    public class FoodProductsController : ApiController
    {
        private readonly IFoodProductRepository _foodProductRepository;
        private readonly FoodProductsService _foodProdSrv;

        public FoodProductsController()
        {
            _foodProductRepository = new FoodProductRepository(new ApplicationDbContext());
            _foodProdSrv = new FoodProductsService(_foodProductRepository);
        }

        public FoodProductsController(IFoodProductRepository foodProductRepository)
        {
            _foodProductRepository = foodProductRepository;
            _foodProdSrv = new FoodProductsService(_foodProductRepository);
        }

        // GET: api/FoodProducts
        //[Authorize]
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IEnumerable<FoodProductDTO>))]
        public async Task<IEnumerable<FoodProductDTO>> GetFoodProductsAsync([FromUri] FoodProductQueryParams queryParams)
        {
            var products = await _foodProdSrv.GetFoodProductsAsync(queryParams);

            return products;
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
        public async Task<IHttpActionResult> PutFoodProduct(int id, ProductEditDTO foodProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != foodProduct.Id)
            {
                return BadRequest();
            }
            if (await _foodProdSrv.PutFoodProductAsync(foodProduct))
            {
                return Ok();
            }

            return StatusCode(HttpStatusCode.BadRequest);
        }

        // POST: api/FoodProducts
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(ProductInsertDTO))]
        public async Task<IHttpActionResult> PostFoodProduct(ProductInsertDTO foodProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var insertedId = await _foodProdSrv.PostFoodProductAsync(foodProduct);

            return CreatedAtRoute("GetFoodProductById", new { id = insertedId }, foodProduct);
        }

        // DELETE: api/FoodProducts/5
        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof(FoodProductDTO))]
        public async Task<IHttpActionResult> DeleteFoodProduct(int id)
        {

            var foodProduct = await _foodProductRepository.GetGetFoodProductByIDAsync(id);//zmienic na check if exists
            if (foodProduct == null)
            {
                return NotFound();
            }

            if(_foodProductRepository.DeleteFoodProduct(foodProduct))
            {
                return Ok(foodProduct);
            }

            return BadRequest("Usuwanie nie powiodło sie");
        }

        //private bool FoodProductExists(int id)
        //{
        //    return db.FoodProducts.Count(e => e.Id == id) > 0;
        //}
    }
}
