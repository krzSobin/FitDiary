namespace FitDiary.SecuredApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Excerciseschanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExcerciseSeries", "TimeInMinutes", c => c.Double(nullable: false));
            DropColumn("dbo.Excercises", "TimeInMinutes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Excercises", "TimeInMinutes", c => c.Double(nullable: false));
            DropColumn("dbo.ExcerciseSeries", "TimeInMinutes");
        }
    }
}
