namespace FitDiary.Api.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FitDiary.Api.DAL.FitDiaryApiContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FitDiary.Api.DAL.FitDiaryApiContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var fCategories = new List<FoodProductCategory>
            {
                new FoodProductCategory {Name = "Miêso i ryby" },
                new FoodProductCategory {Name = "Zbo¿a" },
                new FoodProductCategory {Name = "Warzywa" },
                new FoodProductCategory {Name = "Nabia³" }
            };
            fCategories.ForEach(f => context.FoodProductCategories.AddOrUpdate(f));
            context.SaveChanges();

            var foodProducts = new List<FoodProduct>
            {
                new FoodProduct {Name = "Pierœ z kurczaka", ProteinsPer100g = 21, FatsPer100g = 8, CarboPer100g = 5, SugarPer100g = 0.5, CategoryId = 1, KCalPer100g = 300},
                new FoodProduct {Name = "Pierœ z indyka", ProteinsPer100g = 21, FatsPer100g = 8, CarboPer100g = 5, SugarPer100g = 0.5, CategoryId = 1, KCalPer100g = 300},
                new FoodProduct {Name = "Udko z kurczaka", ProteinsPer100g = 18, FatsPer100g = 12, CarboPer100g = 5, SugarPer100g = 0.5, CategoryId = 1, KCalPer100g = 350},
                new FoodProduct {Name = "Kasza gryczana", ProteinsPer100g = 2, FatsPer100g = 6, CarboPer100g = 76, SugarPer100g = 0.5, CategoryId = 2, KCalPer100g = 400},
                new FoodProduct {Name = "Ry¿ bia³y", ProteinsPer100g = 3, FatsPer100g = 4, CarboPer100g = 77, SugarPer100g = 0.5, CategoryId = 2, KCalPer100g = 500},
                new FoodProduct {Name = "Ziemniaki", ProteinsPer100g = 4, FatsPer100g = 3, CarboPer100g = 65, SugarPer100g = 5, CategoryId = 2, KCalPer100g = 400}
            };
            foodProducts.ForEach(f => context.FoodProducts.AddOrUpdate(f));
            context.SaveChanges();

            var meals = new List<Meal>
            {
                new Meal {Date = DateTime.UtcNow },
                new Meal {Date = DateTime.UtcNow },
                new Meal {Date = DateTime.UtcNow }
            };
            meals.ForEach(m => context.Meals.AddOrUpdate(m));
            context.SaveChanges();

            var prodInMeals = new List<ProductInMeal>
            {
                new ProductInMeal { ProductId = 1, MealId = 1, AmountInGrams = 400},
                new ProductInMeal { ProductId = 4, MealId = 1, AmountInGrams = 100},
                new ProductInMeal { ProductId = 2, MealId = 4, AmountInGrams = 350},
                new ProductInMeal { ProductId = 5, MealId = 4, AmountInGrams = 120}
            };
            prodInMeals.ForEach(p => context.ProductsInMeal.AddOrUpdate(p));
            context.SaveChanges();
        }
    }
}
