using AutoMapper;
using FitDiary.Contracts.DTOs.Diet.FoodProductCategories;
using FitDiary.SecuredApi.Diet.DAL.FoodProductCategories;
using System.Collections.Generic;

namespace FitDiary.SecuredApi.Diet.BLL.FoodProductCategories
{
    public class FoodProductCategoriesService
    {
        private readonly IFoodProductCategoryRepository _categoryRepository;

        public FoodProductCategoriesService(IFoodProductCategoryRepository foodProductCategoryRepository)
        {
            _categoryRepository = foodProductCategoryRepository;
        }

        public IEnumerable<CategorySelectDTO> GetCategories()
        {
            var categoryEntities = _categoryRepository.GetCategories();

            return Mapper.Map<IEnumerable<CategorySelectDTO>>(categoryEntities);
        }
    }
}