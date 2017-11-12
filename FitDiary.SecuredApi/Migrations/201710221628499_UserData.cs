namespace FitDiary.SecuredApi.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UserData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserDatas",
                c => new
                    {
                        UserDataId = c.Int(nullable: false),
                        Birthday = c.DateTime(),
                        Hobby = c.String(),
                        City = c.String(),
                        HeightInCm = c.Int(),
                    })
                .PrimaryKey(t => t.UserDataId);
            
            AddColumn("dbo.AspNetUsers", "AdditionalData_UserDataId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "AdditionalData_UserDataId");
            AddForeignKey("dbo.AspNetUsers", "AdditionalData_UserDataId", "dbo.UserDatas", "UserDataId");
            DropColumn("dbo.AspNetUsers", "Birthday");
            DropColumn("dbo.AspNetUsers", "HeightInCm");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "HeightInCm", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Birthday", c => c.DateTime());
            DropForeignKey("dbo.AspNetUsers", "AdditionalData_UserDataId", "dbo.UserDatas");
            DropIndex("dbo.AspNetUsers", new[] { "AdditionalData_UserDataId" });
            DropColumn("dbo.AspNetUsers", "AdditionalData_UserDataId");
            DropTable("dbo.UserDatas");
        }
    }
}
