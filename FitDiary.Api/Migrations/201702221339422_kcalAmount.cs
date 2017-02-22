namespace FitDiary.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kcalAmount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FoodProducts", "KCalPer100g", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FoodProducts", "KCalPer100g");
        }
    }
}
