namespace FitDiary.SecuredApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Workout : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Workouts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Excercises", "WorkoutId", c => c.Int(nullable: false));
            CreateIndex("dbo.Excercises", "WorkoutId");
            AddForeignKey("dbo.Excercises", "WorkoutId", "dbo.Workouts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Excercises", "WorkoutId", "dbo.Workouts");
            DropIndex("dbo.Excercises", new[] { "WorkoutId" });
            DropColumn("dbo.Excercises", "WorkoutId");
            DropTable("dbo.Workouts");
        }
    }
}
