using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using FitDiary.SecuredApi.Models.Diet;
using System.Data.Entity;

namespace FitDiary.SecuredApi.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public int Age { get; set; }
        public int HeightInCm { get; set; }

        public double Weight { get; set; }
        public double BodyFat { get; set; }

        public DateTime JoinDate { get; set; } = DateTime.UtcNow;

        public int BodyMeasurementsId { get; set; }
        public virtual BodyMeasurements BodyMeasurements { get; set; }
        //public virtual BodyGoals BodyGoals { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("FitDiarySecuredApiContext", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<FoodProduct> FoodProducts { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<FoodProductCategory> FoodProductCategories { get; set; }
        public DbSet<ProductInMeal> ProductsInMeal { get; set; }
        public DbSet<BodyMeasurements> BodyMeasurements { get; set; }
    }
}