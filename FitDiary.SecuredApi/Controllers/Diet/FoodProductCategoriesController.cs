using FitDiary.Contracts.DTOs.Diet.FoodProductCategories;
using FitDiary.SecuredApi.Diet.BLL.FoodProductCategories;
using FitDiary.SecuredApi.Diet.DAL.FoodProductCategories;
using FitDiary.SecuredApi.Models;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace FitDiary.SecuredApi.Controllers.Diet
{
    [RoutePrefix("api/foodCategories")]
    public class FoodProductCategoriesController : ApiController
    {
        private readonly FoodProductCategoriesService _foodProductCategoriesService;

        public FoodProductCategoriesController()
        {
            var foodProductCategoryRepository = new FoodProductCategoryRepository(new ApplicationDbContext());
            _foodProductCategoriesService = new FoodProductCategoriesService(foodProductCategoryRepository);
        }

        public FoodProductCategoriesController(IFoodProductCategoryRepository foodProductCategoryRepository)
        {
            _foodProductCategoriesService = new FoodProductCategoriesService(foodProductCategoryRepository);
        }

        // GET: api/foodCategories
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IEnumerable<CategorySelectDTO>))]
        public IEnumerable<CategorySelectDTO> GetCategories()
        {
            return _foodProductCategoriesService.GetCategories();
        }
    }
}