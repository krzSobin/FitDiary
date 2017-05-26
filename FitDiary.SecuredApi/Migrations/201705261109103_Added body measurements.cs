namespace FitDiary.SecuredApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedbodymeasurements : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BodyMeasurements",
                c => new
                    {
                        Id = c.Double(nullable: false),
                        WeightInKg = c.Double(nullable: false),
                        ChestInCm = c.Double(nullable: false),
                        WaistInCm = c.Double(nullable: false),
                        MeasurementDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "BodyMeasurementId", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Measurements_Id", c => c.Double());
            CreateIndex("dbo.AspNetUsers", "Measurements_Id");
            AddForeignKey("dbo.AspNetUsers", "Measurements_Id", "dbo.BodyMeasurements", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Measurements_Id", "dbo.BodyMeasurements");
            DropIndex("dbo.AspNetUsers", new[] { "Measurements_Id" });
            DropColumn("dbo.AspNetUsers", "Measurements_Id");
            DropColumn("dbo.AspNetUsers", "BodyMeasurementId");
            DropTable("dbo.BodyMeasurements");
        }
    }
}
