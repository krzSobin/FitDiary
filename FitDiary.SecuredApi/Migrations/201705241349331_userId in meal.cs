namespace FitDiary.SecuredApi.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class userIdinmeal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Meals", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Meals", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Meals", "User_Id");
            AddForeignKey("dbo.Meals", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Meals", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Meals", new[] { "User_Id" });
            DropColumn("dbo.Meals", "User_Id");
            DropColumn("dbo.Meals", "UserId");
        }
    }
}
