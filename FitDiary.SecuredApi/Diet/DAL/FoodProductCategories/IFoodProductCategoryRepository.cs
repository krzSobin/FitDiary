using FitDiary.SecuredApi.Models.Diet;
using System;
using System.Collections.Generic;

namespace FitDiary.SecuredApi.Diet.DAL.FoodProductCategories
{
    public interface IFoodProductCategoryRepository : IDisposable
    {
        FoodProductCategory GetCategoryById(int categoryId);
        IEnumerable<FoodProductCategory> GetCategories();
    }
}