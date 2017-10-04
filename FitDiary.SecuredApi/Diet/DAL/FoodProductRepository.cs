using FitDiary.SecuredApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FitDiary.SecuredApi.Models.Diet;
using System.Threading.Tasks;

namespace FitDiary.SecuredApi.Diet.DAL
{
    public class FoodProductRepository : IFoodProductRepository
    {
        private ApplicationDbContext context;

        public FoodProductRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void DeleteFoodProduct(int foodProductId)
        {
            var foodProduct = context.FoodProducts.Find(foodProductId);
            context.FoodProducts.Remove(foodProduct);
        }

        public async Task<IEnumerable<FoodProduct>> GetFoodProductsAsync()
        {
            return context.FoodProducts.ToList();
        }

        public async Task<IEnumerable<FoodProduct>> GetFoodProductsAsync(FoodProductQueryParams queryParams)
        {
            if (queryParams == null)
            {
                throw new ArgumentNullException(nameof(queryParams));
            }

            var productQuery = context.FoodProducts
                .Where(fp => queryParams.Category == null || queryParams.Category.Trim() == string.Empty || fp.Category.Name == queryParams.Category)
                .Where(fp => queryParams.Name == null || queryParams.Name.Trim() == string.Empty || fp.Name.ToLower().Contains(queryParams.Name))
                .Where(fp => queryParams.MaxSugar == null || fp.SugarPer100g <= queryParams.MaxSugar);

            if (queryParams.SortColumn == SortColumn.SortByName)
                productQuery = queryParams.SortOrder == System.Data.SqlClient.SortOrder.Descending ? productQuery.OrderByDescending(fp => fp.Name) : productQuery.OrderBy(fp => fp.Name);
            else
                productQuery = queryParams.SortOrder == System.Data.SqlClient.SortOrder.Descending ? productQuery.OrderByDescending(fp => fp.KCalPer100g) : productQuery.OrderBy(fp => fp.KCalPer100g);

            return productQuery.ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        #region IDisposable Support
        private bool disposed = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposed = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~FoodProductRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}