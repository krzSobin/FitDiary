using FitDiary.SecuredApi.Models.Diet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FitDiary.SecuredApi.Diet.DAL
{
    public interface IFoodProductRepository : IDisposable
    {
        Task<IEnumerable<FoodProduct>> GetFoodProductsAsync(FoodProductQueryParams queryParams);
        Task<IEnumerable<FoodProduct>> GetFoodProductsAsync();
        Task<FoodProduct> GetGetFoodProductByIDAsync(int foodProductId);
        bool DeleteFoodProduct(FoodProduct foodProduct);
        void Save();
    }
}