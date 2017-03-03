namespace FitDiary.Api.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class previous : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.FoodProducts", "KCalPer100g");
            DropColumn("dbo.Meals", "TotalKCal");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Meals", "TotalKCal", c => c.Double(nullable: false));
            AddColumn("dbo.FoodProducts", "KCalPer100g", c => c.Double(nullable: false));
        }
    }
}
