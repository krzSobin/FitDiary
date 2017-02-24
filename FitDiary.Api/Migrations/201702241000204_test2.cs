namespace FitDiary.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Meals", "TotalKcal", c => c.Double(nullable: false));
            AddColumn("dbo.Meals", "TotalProtein", c => c.Double(nullable: false));
            AddColumn("dbo.Meals", "TotalFat", c => c.Double(nullable: false));
            AddColumn("dbo.Meals", "TotalCarb", c => c.Double(nullable: false));
            AddColumn("dbo.Meals", "TotalSugar", c => c.Double(nullable: false));
            CreateIndex("dbo.ProductInMeals", "ProductId");
            AddForeignKey("dbo.ProductInMeals", "ProductId", "dbo.FoodProducts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductInMeals", "ProductId", "dbo.FoodProducts");
            DropIndex("dbo.ProductInMeals", new[] { "ProductId" });
            DropColumn("dbo.Meals", "TotalSugar");
            DropColumn("dbo.Meals", "TotalCarb");
            DropColumn("dbo.Meals", "TotalFat");
            DropColumn("dbo.Meals", "TotalProtein");
            DropColumn("dbo.Meals", "TotalKcal");
        }
    }
}
