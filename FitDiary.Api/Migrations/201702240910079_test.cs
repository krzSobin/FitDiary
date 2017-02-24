namespace FitDiary.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductInMeals", "FoodProduct_Id", "dbo.FoodProducts");
            DropIndex("dbo.ProductInMeals", new[] { "FoodProduct_Id" });
            DropColumn("dbo.ProductInMeals", "FoodProduct_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductInMeals", "FoodProduct_Id", c => c.Int());
            CreateIndex("dbo.ProductInMeals", "FoodProduct_Id");
            AddForeignKey("dbo.ProductInMeals", "FoodProduct_Id", "dbo.FoodProducts", "Id");
        }
    }
}
