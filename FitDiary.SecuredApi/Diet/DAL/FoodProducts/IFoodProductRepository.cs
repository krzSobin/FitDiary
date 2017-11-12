using FitDiary.SecuredApi.Models.Diet;
using System;
using System.Collections.Generic;

namespace FitDiary.SecuredApi.Diet.DAL.FoodProducts
{
    public interface IFoodProductRepository : IDisposable
    {
        IEnumerable<FoodProduct> GetFoodProducts(FoodProductQueryParams queryParams);
        IEnumerable<FoodProduct> GetFoodProducts();
        FoodProduct GetFoodProductByID(int foodProductId);
        FoodProduct InsertFoodProduct(FoodProduct foodProduct);
        void UpdateFoodProduct(FoodProduct foodProduct);
        bool DeleteFoodProduct(FoodProduct foodProduct);
        bool Save();
    }
}