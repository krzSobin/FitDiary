namespace FitDiary.SecuredApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManyMeasurementsPerUser : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AspNetUsers", new[] { "BodyMeasurements_Id" });
            AddColumn("dbo.BodyMeasurements", "User_Id", c => c.Int());
            CreateIndex("dbo.BodyMeasurements", "User_Id");
            DropColumn("dbo.AspNetUsers", "BodyMeasurements_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "BodyMeasurements_Id", c => c.Double());
            DropIndex("dbo.BodyMeasurements", new[] { "User_Id" });
            DropColumn("dbo.BodyMeasurements", "User_Id");
            CreateIndex("dbo.AspNetUsers", "BodyMeasurements_Id");
        }
    }
}
