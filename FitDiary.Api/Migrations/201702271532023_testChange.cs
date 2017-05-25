namespace FitDiary.Api.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class testChange : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Excercises", "Reps");
            DropColumn("dbo.Excercises", "Series");
            DropColumn("dbo.Excercises", "Weight");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Excercises", "Weight", c => c.Double(nullable: false));
            AddColumn("dbo.Excercises", "Series", c => c.Int(nullable: false));
            AddColumn("dbo.Excercises", "Reps", c => c.Int(nullable: false));
        }
    }
}
