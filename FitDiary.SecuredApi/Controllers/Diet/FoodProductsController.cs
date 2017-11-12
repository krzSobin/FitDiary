using FitDiary.Contracts.DTOs.Diet;
using FitDiary.Contracts.DTOs.Diet.FoodProducts;
using FitDiary.SecuredApi.Diet.BLL.FoodProducts;
using FitDiary.SecuredApi.Diet.DAL.FoodProducts;
using FitDiary.SecuredApi.Models;
using FitDiary.SecuredApi.Models.Diet;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace FitDiary.SecuredApi.Controllers.Diet
{
    [RoutePrefix("api/foodProducts")]
    public class FoodProductsController : ApiController
    {
        private readonly FoodProductsService _foodProductService;

        public FoodProductsController()
        {
            var foodProductRepository = new FoodProductRepository(new ApplicationDbContext());
            _foodProductService = new FoodProductsService(foodProductRepository);
        }

        public FoodProductsController(IFoodProductRepository foodProductRepository)
        {
            _foodProductService = new FoodProductsService(foodProductRepository);
        }

        // GET: api/FoodProducts
        //[Authorize]
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IEnumerable<FoodProductDTO>))]
        public IEnumerable<FoodProductDTO> GetFoodProducts([FromUri] FoodProductQueryParams queryParams)
        {
            var products = _foodProductService.GetFoodProducts(queryParams);

            return products;
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetFoodProductById")]
        [ResponseType(typeof(FoodProductDTO))]
        public IHttpActionResult GetFoodProduct(int id)
        {
            var product = _foodProductService.GetFoodProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/FoodProducts/5
        [HttpPut]
        [Route("{id:int}")]
        [ResponseType(typeof(UpdateFoodProductResultDTO))]
        public async Task<IHttpActionResult> PutFoodProduct(int id, UpdateProductDTO foodProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != foodProduct.Id)
            {
                return BadRequest();
            }

            var result = _foodProductService.UpdateFoodProduct(foodProduct);
            if (result.Updated)
            {
                return Ok(result);
            }

            return StatusCode(HttpStatusCode.BadRequest);
        }

        // POST: api/FoodProducts
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(AddFoodProductResultDTO))]
        public IHttpActionResult PostFoodProduct(ProductInsertDTO foodProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _foodProductService.PostFoodProduct(foodProduct);
            if (result.Added)
            {
                return CreatedAtRoute("GetFoodProductById", new { id = result.FoodProduct.Id }, result);
            }

            return BadRequest("Adding foodProduct error");
        }

        // DELETE: api/FoodProducts/5
        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof(DeleteFoodProductResultDTO))]
        public IHttpActionResult DeleteFoodProduct(int id)
        {
            var result = _foodProductService.DeleteFoodProduct(id);
            if (result == null)
            {
                return NotFound();
            }

            if (result.Deleted)
            {
                return Ok(result);
            }

            return BadRequest("Delete foodProduct error");
        }
    }
}
