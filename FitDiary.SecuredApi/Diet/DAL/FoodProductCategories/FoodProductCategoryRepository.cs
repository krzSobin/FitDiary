using System.Collections.Generic;
using System.Linq;
using FitDiary.SecuredApi.Models.Diet;
using FitDiary.SecuredApi.Models;

namespace FitDiary.SecuredApi.Diet.DAL.FoodProductCategories
{
    public class FoodProductCategoryRepository : IFoodProductCategoryRepository
    {
        private ApplicationDbContext context;

        public FoodProductCategoryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<FoodProductCategory> GetCategories()
        {
            return context.FoodProductCategories.OrderBy(c => c.Name).ToList();
        }

        public FoodProductCategory GetCategoryById(int categoryId)
        {
            return context.FoodProductCategories.FirstOrDefault(c => c.Id == categoryId);
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
        // ~FoodProductCategoryRepository() {
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