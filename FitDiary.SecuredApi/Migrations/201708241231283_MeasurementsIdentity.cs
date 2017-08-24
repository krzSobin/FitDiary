namespace FitDiary.SecuredApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MeasurementsIdentity : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.BodyMeasurements");
            AlterColumn("dbo.BodyMeasurements", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.BodyMeasurements", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.BodyMeasurements");
            AlterColumn("dbo.BodyMeasurements", "Id", c => c.Double(nullable: false));
            AddPrimaryKey("dbo.BodyMeasurements", "Id");
        }
    }
}
