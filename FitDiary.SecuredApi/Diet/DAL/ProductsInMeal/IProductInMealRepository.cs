using FitDiary.SecuredApi.Models.Diet;
using System;

namespace FitDiary.SecuredApi.Diet.DAL.ProductsInMeal
{
    public interface IProductInMealRepository : IDisposable
    {
        ProductInMeal GetProductInMeal(int id);
        bool ProductInMealExists(int id);
    }
}
