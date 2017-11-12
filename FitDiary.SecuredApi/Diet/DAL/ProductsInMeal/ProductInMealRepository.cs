using System.Linq;
using FitDiary.SecuredApi.Models.Diet;
using FitDiary.SecuredApi.Models;

namespace FitDiary.SecuredApi.Diet.DAL.ProductsInMeal
{
    public class ProductInMealRepository : IProductInMealRepository
    {
        private ApplicationDbContext context;

        public ProductInMealRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public ProductInMeal GetProductInMeal(int id)
        {
            return context.ProductsInMeal.FirstOrDefault(p => p.Id == id);
        }

        public bool ProductInMealExists(int id)
        {
            return context.ProductsInMeal.Any(p => p.Id == id);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ProductInMealRepository() {
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