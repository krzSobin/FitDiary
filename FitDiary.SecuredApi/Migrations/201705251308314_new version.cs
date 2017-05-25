namespace FitDiary.SecuredApi.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class newversion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Meals", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Meals", "Name");
        }
    }
}
