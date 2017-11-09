using Microsoft.AspNet.Identity.EntityFramework;
using FitDiary.SecuredApi.Models.Diet;
using System.Data.Entity;
using FitDiary.SecuredApi.Models.Training;
using FitDiary.SecuredApi.Models.User;
using FitDiary.SecuredApi.User.Models;

namespace FitDiary.SecuredApi.Models
{
    

    public class CustomUserRole : IdentityUserRole<int> { }
    public class CustomUserClaim : IdentityUserClaim<int> { }
    public class CustomUserLogin : IdentityUserLogin<int> { }

    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }
    }

    public class CustomUserStore : UserStore<ApplicationUser, CustomRole, int,
        CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    {
        public CustomRoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public ApplicationDbContext()
            : base("FitDiarySecuredApiContext")
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
        public DbSet<BodyGoals> BodyGoals { get; set; }
        public DbSet<UserData> UserData { get; set; }

        public DbSet<Excercise> Excercises { get; set; }
        public DbSet<ExcerciseSerie> ExcerciseSeries { get; set; }
        public DbSet<Muscle> Muscles { get; set; }
        public DbSet<MuscleInExcercise> MusclesInExcercise { get; set; }
    }
}