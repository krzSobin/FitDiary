namespace FitDiary.SecuredApi.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Fixbodymeasurementv2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "BodyMeasurementsId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "BodyMeasurementsId", c => c.Int(nullable: false));
        }
    }
}
