namespace FitDiary.SecuredApi.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Lessinusertemporary : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Measurements_WeightInKg");
            DropColumn("dbo.AspNetUsers", "Measurements_ChestInCm");
            DropColumn("dbo.AspNetUsers", "Measurements_WaistInCm");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Measurements_WaistInCm", c => c.Double(nullable: false));
            AddColumn("dbo.AspNetUsers", "Measurements_ChestInCm", c => c.Double(nullable: false));
            AddColumn("dbo.AspNetUsers", "Measurements_WeightInKg", c => c.Double(nullable: false));
        }
    }
}
