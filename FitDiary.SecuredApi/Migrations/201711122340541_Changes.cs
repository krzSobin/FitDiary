namespace FitDiary.SecuredApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FoodProducts", "CarbsPer100g", c => c.Double(nullable: false));
            DropColumn("dbo.FoodProducts", "CarboPer100g");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FoodProducts", "CarboPer100g", c => c.Double(nullable: false));
            DropColumn("dbo.FoodProducts", "CarbsPer100g");
        }
    }
}
