namespace FitDiary.SecuredApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Useridasstring : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Meals", new[] { "User_Id" });
            DropColumn("dbo.Meals", "UserId");
            RenameColumn(table: "dbo.Meals", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.Meals", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Meals", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Meals", new[] { "UserId" });
            AlterColumn("dbo.Meals", "UserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Meals", name: "UserId", newName: "User_Id");
            AddColumn("dbo.Meals", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Meals", "User_Id");
        }
    }
}
