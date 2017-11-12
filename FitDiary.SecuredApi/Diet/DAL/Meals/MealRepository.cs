using FitDiary.Contracts.DTOs.Diet;
using FitDiary.SecuredApi.Models;
using FitDiary.SecuredApi.Models.Diet;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace FitDiary.SecuredApi.Diet.DAL.Meals
{
    public class MealRepository : IMealRepository
    {
        private ApplicationDbContext context;

        public MealRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Meal AddMeal(Meal meal)
        {
           return context.Meals.Add(meal);
        }

        public void UpdateMeal(Meal meal)
        {
            context.Entry(meal).State = EntityState.Modified;
        }

        public IEnumerable<Meal> GetMeals(int userId)
        {
            return context.Meals.Where(m => m.User.Id == userId).ToList();
        }

        public Meal GetMealById(int id)
        {
            return context.Meals.FirstOrDefault(m => m.Id == id);
        }

        public IEnumerable<Meal> GetMealsByDate(DateTime date, int userId)
        {
            return context.Meals.Where(m => m.Date.Date == date.Date && m.User.Id == userId);
        }

        public IEnumerable<MealForListingDTO> GetMealsByDay()
        {
            throw new NotImplementedException();
        }

        public void DeleteMeal(Meal meal)
        {
            context.Meals.Remove(meal);
        }

        public bool Save()
        {
            try
            {
                return context.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
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
        // ~MealRepository() {
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