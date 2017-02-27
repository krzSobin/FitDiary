using FitDiary.Api.Models;
using System.Data.Entity;

namespace FitDiary.Api.DAL
{
    public class FitDiaryApiContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public FitDiaryApiContext() : base("name=FitDiaryApiContext")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Meal> Meals { get; set; }

        public DbSet<Training> Trainings { get; set; }

        public DbSet<FoodProduct> FoodProducts { get; set; }

        public DbSet<FoodProductCategory> FoodProductCategories { get; set; }

        public DbSet<Excercise> Excercises { get; set; }

        public DbSet<ExcerciseCategory> ExcerciseCategories { get; set; }

        public DbSet<ProductInMeal> ProductsInMeal { get; set; }
    }
}
