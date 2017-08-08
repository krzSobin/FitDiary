﻿using FitDiary.Contracts.DTOs.Diet.FoodProductCategories;
using FitDiary.SecuredApi.Services.Diet;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FitDiary.SecuredApi.Controllers.Diet
{
    [RoutePrefix("api/foodCategories")]
    public class FoodProductCategoriesController : ApiController
    {
        private readonly FoodProductCategoriesService _categorySrv = new FoodProductCategoriesService();

        // GET: api/foodCategories
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<CategorySelectDTO>> GetCategoriesAsync()
        {
            var meals = await _categorySrv.GetCategoriesAsync();

            return meals;
        }
    }
}