namespace FitDiary.SecuredApi.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Changes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Measurements_WeightInKg", c => c.Double(nullable: false));
            AddColumn("dbo.AspNetUsers", "Measurements_ChestInCm", c => c.Double(nullable: false));
            AddColumn("dbo.AspNetUsers", "Measurements_WaistInCm", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Measurements_WaistInCm");
            DropColumn("dbo.AspNetUsers", "Measurements_ChestInCm");
            DropColumn("dbo.AspNetUsers", "Measurements_WeightInKg");
        }
    }
}
