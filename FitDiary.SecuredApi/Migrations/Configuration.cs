using FitDiary.SecuredApi.Models;
using FitDiary.SecuredApi.Models.User;

namespace FitDiary.SecuredApi.Migrations
{
    using Models.Diet;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //using (var manager = new ApplicationUserManager(new CustomUserStore(context)))
            //{
            //    var user = new ApplicationUser
            //    {
            //        UserName = "User1",
            //        Email = "test1@test.pl",
            //        EmailConfirmed = true,
            //        JoinDate = DateTime.Now.AddYears(-3)
            //    };
            //    await manager.CreateAsync(user, "Test123_");

            //    user = new ApplicationUser
            //    {
            //        UserName = "User2",
            //        Email = "test2@test.pl",
            //        EmailConfirmed = true,
            //        JoinDate = DateTime.Now.AddYears(-1)
            //    };

            //    await manager.CreateAsync(user, "Test123_");
            //}
            IList<FoodProductCategory> categories = new List<FoodProductCategory>();

            categories.Add(new FoodProductCategory
            {
                Id = 1,
                Name = "miêso"
            });
            categories.Add(new FoodProductCategory
            {
                Id = 2,
                Name = "zbo¿a"
            });
            categories.Add(new FoodProductCategory
            {
                Id = 3,
                Name = "napoje"
            });
            categories.Add(new FoodProductCategory
            {
                Id = 4,
                Name = "warzywa"
            });
            categories.Add(new FoodProductCategory
            {
                Id = 5,
                Name = "ryby"
            });
            categories.Add(new FoodProductCategory
            {
                Id = 6,
                Name = "owoce"
            });

            foreach (var category in categories)
                context.FoodProductCategories.AddOrUpdate(category);

            context.SaveChanges();

            IList<FoodProduct> products = new List<FoodProduct>();

            products.Add(new FoodProduct
            {
                Id = 1,
                Name = "Wata cukrowa",
                ProteinsPer100g = 1,
                FatsPer100g = 0,
                CarboPer100g = 98,
                SugarPer100g = 98,
                CategoryId = 1,
                KCalPer100g = 400
            });
            products.Add(new FoodProduct
            {
                Id = 2,
                Name = "Pierœ z indyka",
                ProteinsPer100g = 23,
                FatsPer100g = 7,
                CarboPer100g = 6,
                SugarPer100g = 2,
                CategoryId = 1,
                KCalPer100g = 370
            });
            products.Add(new FoodProduct
            {
                Id = 3,
                Name = "Ry¿ bia³y",
                ProteinsPer100g = 5,
                FatsPer100g = 7,
                CarboPer100g = 53,
                SugarPer100g = 12,
                CategoryId = 5,
                KCalPer100g = 334
            });
            products.Add(new FoodProduct
            {
                Id = 4,
                Name = "Ry¿ br¹zowy",
                ProteinsPer100g = 7,
                FatsPer100g = 4,
                CarboPer100g = 63,
                SugarPer100g = 10,
                CategoryId = 5,
                KCalPer100g = 310
            });
            products.Add(new FoodProduct
            {
                Id = 5,
                Name = "£osoœ",
                ProteinsPer100g = 22,
                FatsPer100g = 14,
                CarboPer100g = 13,
                SugarPer100g = 2,
                CategoryId = 2,
                KCalPer100g = 390
            });
            products.Add(new FoodProduct
            {
                Id = 6,
                Name = "Tuñczyk",
                ProteinsPer100g = 24,
                FatsPer100g = 12,
                CarboPer100g = 11,
                SugarPer100g = 4,
                CategoryId = 2,
                KCalPer100g = 380
            });

            foreach (var product in products)
                context.FoodProducts.AddOrUpdate(product);

            context.SaveChanges();
        }
    }
}
