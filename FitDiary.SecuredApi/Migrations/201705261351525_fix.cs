namespace FitDiary.SecuredApi.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class fix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "BodyMeasurements_Id", "dbo.BodyMeasurements");
            DropPrimaryKey("dbo.BodyMeasurements");
            AddColumn("dbo.AspNetUsers", "BodyMeasurementsId", c => c.Int(nullable: false));
            AlterColumn("dbo.BodyMeasurements", "Id", c => c.Double(nullable: false));
            AlterColumn("dbo.BodyMeasurements", "WeightInKg", c => c.Double());
            AlterColumn("dbo.BodyMeasurements", "ChestInCm", c => c.Double());
            AlterColumn("dbo.BodyMeasurements", "WaistInCm", c => c.Double());
            AddPrimaryKey("dbo.BodyMeasurements", "Id");
            AddForeignKey("dbo.AspNetUsers", "BodyMeasurements_Id", "dbo.BodyMeasurements", "Id");
            DropColumn("dbo.Meals", "TotalKcal");
            DropColumn("dbo.Meals", "TotalProtein");
            DropColumn("dbo.Meals", "TotalFat");
            DropColumn("dbo.Meals", "TotalCarb");
            DropColumn("dbo.Meals", "TotalSugar");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Meals", "TotalSugar", c => c.Double(nullable: false));
            AddColumn("dbo.Meals", "TotalCarb", c => c.Double(nullable: false));
            AddColumn("dbo.Meals", "TotalFat", c => c.Double(nullable: false));
            AddColumn("dbo.Meals", "TotalProtein", c => c.Double(nullable: false));
            AddColumn("dbo.Meals", "TotalKcal", c => c.Double(nullable: false));
            DropForeignKey("dbo.AspNetUsers", "BodyMeasurements_Id", "dbo.BodyMeasurements");
            DropPrimaryKey("dbo.BodyMeasurements");
            AlterColumn("dbo.BodyMeasurements", "WaistInCm", c => c.Double(nullable: false));
            AlterColumn("dbo.BodyMeasurements", "ChestInCm", c => c.Double(nullable: false));
            AlterColumn("dbo.BodyMeasurements", "WeightInKg", c => c.Double(nullable: false));
            AlterColumn("dbo.BodyMeasurements", "Id", c => c.Double(nullable: false));
            DropColumn("dbo.AspNetUsers", "BodyMeasurementsId");
            AddPrimaryKey("dbo.BodyMeasurements", "Id");
            AddForeignKey("dbo.AspNetUsers", "BodyMeasurements_Id", "dbo.BodyMeasurements", "Id");
        }
    }
}
