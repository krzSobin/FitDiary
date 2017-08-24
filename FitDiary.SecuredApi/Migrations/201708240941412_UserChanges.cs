namespace FitDiary.SecuredApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BodyMeasurements", "BodyFat", c => c.Double());
            AddColumn("dbo.AspNetUsers", "Birthday", c => c.DateTime());
            AlterColumn("dbo.AspNetUsers", "HeightInCm", c => c.Int());
            DropColumn("dbo.AspNetUsers", "Age");
            DropColumn("dbo.AspNetUsers", "Weight");
            DropColumn("dbo.AspNetUsers", "BodyFat");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "BodyFat", c => c.Double(nullable: false));
            AddColumn("dbo.AspNetUsers", "Weight", c => c.Double(nullable: false));
            AddColumn("dbo.AspNetUsers", "Age", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "HeightInCm", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "Birthday");
            DropColumn("dbo.BodyMeasurements", "BodyFat");
        }
    }
}
