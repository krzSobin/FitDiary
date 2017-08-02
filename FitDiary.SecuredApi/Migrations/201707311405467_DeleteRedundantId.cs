namespace FitDiary.SecuredApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteRedundantId : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Meals", "UserId");
            DropColumn("dbo.AspNetUsers", "BodyMeasurementsId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "BodyMeasurementsId", c => c.Int(nullable: false));
            AddColumn("dbo.Meals", "UserId", c => c.String());
        }
    }
}
