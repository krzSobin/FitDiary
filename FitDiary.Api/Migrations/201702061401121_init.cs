namespace FitDiary.Api.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExcerciseCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Excercises",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Reps = c.Int(nullable: false),
                        Series = c.Int(nullable: false),
                        Weight = c.Double(nullable: false),
                        TimeInMinutes = c.Double(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        Training_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExcerciseCategories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Trainings", t => t.Training_Id)
                .Index(t => t.CategoryId)
                .Index(t => t.Training_Id);
            
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
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FoodProductCategories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
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
                "dbo.Meals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trainings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Excercises", "Training_Id", "dbo.Trainings");
            DropForeignKey("dbo.ProductInMeals", "ProductId", "dbo.FoodProducts");
            DropForeignKey("dbo.ProductInMeals", "MealId", "dbo.Meals");
            DropForeignKey("dbo.FoodProducts", "CategoryId", "dbo.FoodProductCategories");
            DropForeignKey("dbo.Excercises", "CategoryId", "dbo.ExcerciseCategories");
            DropIndex("dbo.ProductInMeals", new[] { "MealId" });
            DropIndex("dbo.ProductInMeals", new[] { "ProductId" });
            DropIndex("dbo.FoodProducts", new[] { "CategoryId" });
            DropIndex("dbo.Excercises", new[] { "Training_Id" });
            DropIndex("dbo.Excercises", new[] { "CategoryId" });
            DropTable("dbo.Trainings");
            DropTable("dbo.Meals");
            DropTable("dbo.ProductInMeals");
            DropTable("dbo.FoodProducts");
            DropTable("dbo.FoodProductCategories");
            DropTable("dbo.Excercises");
            DropTable("dbo.ExcerciseCategories");
        }
    }
}
