namespace FitDiary.SecuredApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fixbodymeasurement : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AspNetUsers", name: "Measurements_Id", newName: "BodyMeasurements_Id");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_Measurements_Id", newName: "IX_BodyMeasurements_Id");
            AddColumn("dbo.AspNetUsers", "BodyMeasurementsId", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "BodyMeasurementId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "BodyMeasurementId", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "BodyMeasurementsId");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_BodyMeasurements_Id", newName: "IX_Measurements_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "BodyMeasurements_Id", newName: "Measurements_Id");
        }
    }
}
