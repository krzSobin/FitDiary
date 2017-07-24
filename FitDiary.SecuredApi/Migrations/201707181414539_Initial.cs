namespace FitDiary.SecuredApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BodyGoals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        WeightInKg = c.Double(),
                        ChestInCm = c.Double(),
                        WaistInCm = c.Double(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BodyMeasurements",
                c => new
                    {
                        Id = c.Double(nullable: false),
                        WeightInKg = c.Double(),
                        ChestInCm = c.Double(),
                        WaistInCm = c.Double(),
                        MeasurementDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Excercises",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MuscleInExcercises",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsMainMuscle = c.Boolean(nullable: false),
                        MuscleId = c.Int(nullable: false),
                        ExcerciseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Excercises", t => t.ExcerciseId, cascadeDelete: true)
                .ForeignKey("dbo.Muscles", t => t.MuscleId, cascadeDelete: true)
                .Index(t => t.MuscleId)
                .Index(t => t.ExcerciseId);
            
            CreateTable(
                "dbo.Muscles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExcerciseSeries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Weight = c.Double(nullable: false),
                        Reps = c.Int(nullable: false),
                        TimeInMinutes = c.Double(nullable: false),
                        WorkoutId = c.Int(nullable: false),
                        ExcerciseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Excercises", t => t.ExcerciseId, cascadeDelete: true)
                .ForeignKey("dbo.Workouts", t => t.WorkoutId, cascadeDelete: true)
                .Index(t => t.WorkoutId)
                .Index(t => t.ExcerciseId);
            
            CreateTable(
                "dbo.Workouts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FoodProductCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FoodProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ProteinsPer100g = c.Double(nullable: false),
                        FatsPer100g = c.Double(nullable: false),
                        CarboPer100g = c.Double(nullable: false),
                        SugarPer100g = c.Double(nullable: false),
                        KCalPer100g = c.Double(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FoodProductCategories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Meals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Date = c.DateTime(nullable: false),
                        UserId = c.String(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.ProductInMeals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AmountInGrams = c.Double(nullable: false),
                        ProductId = c.Int(nullable: false),
                        MealId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Meals", t => t.MealId, cascadeDelete: true)
                .ForeignKey("dbo.FoodProducts", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.MealId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Age = c.Int(nullable: false),
                        HeightInCm = c.Int(nullable: false),
                        Weight = c.Double(nullable: false),
                        BodyFat = c.Double(nullable: false),
                        JoinDate = c.DateTime(nullable: false),
                        BodyMeasurementsId = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        BodyMeasurements_Id = c.Double(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BodyMeasurements", t => t.BodyMeasurements_Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.BodyMeasurements_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Meals", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "BodyMeasurements_Id", "dbo.BodyMeasurements");
            DropForeignKey("dbo.ProductInMeals", "ProductId", "dbo.FoodProducts");
            DropForeignKey("dbo.ProductInMeals", "MealId", "dbo.Meals");
            DropForeignKey("dbo.FoodProducts", "CategoryId", "dbo.FoodProductCategories");
            DropForeignKey("dbo.ExcerciseSeries", "WorkoutId", "dbo.Workouts");
            DropForeignKey("dbo.ExcerciseSeries", "ExcerciseId", "dbo.Excercises");
            DropForeignKey("dbo.MuscleInExcercises", "MuscleId", "dbo.Muscles");
            DropForeignKey("dbo.MuscleInExcercises", "ExcerciseId", "dbo.Excercises");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "BodyMeasurements_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.ProductInMeals", new[] { "MealId" });
            DropIndex("dbo.ProductInMeals", new[] { "ProductId" });
            DropIndex("dbo.Meals", new[] { "User_Id" });
            DropIndex("dbo.FoodProducts", new[] { "CategoryId" });
            DropIndex("dbo.ExcerciseSeries", new[] { "ExcerciseId" });
            DropIndex("dbo.ExcerciseSeries", new[] { "WorkoutId" });
            DropIndex("dbo.MuscleInExcercises", new[] { "ExcerciseId" });
            DropIndex("dbo.MuscleInExcercises", new[] { "MuscleId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ProductInMeals");
            DropTable("dbo.Meals");
            DropTable("dbo.FoodProducts");
            DropTable("dbo.FoodProductCategories");
            DropTable("dbo.Workouts");
            DropTable("dbo.ExcerciseSeries");
            DropTable("dbo.Muscles");
            DropTable("dbo.MuscleInExcercises");
            DropTable("dbo.Excercises");
            DropTable("dbo.BodyMeasurements");
            DropTable("dbo.BodyGoals");
        }
    }
}
