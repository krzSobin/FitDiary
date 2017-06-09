namespace FitDiary.SecuredApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Excerciseschangesv3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Excercises", "Workout_Id", "dbo.Workouts");
            DropIndex("dbo.Excercises", new[] { "Workout_Id" });
            DropColumn("dbo.Excercises", "Workout_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Excercises", "Workout_Id", c => c.Int());
            CreateIndex("dbo.Excercises", "Workout_Id");
            AddForeignKey("dbo.Excercises", "Workout_Id", "dbo.Workouts", "Id");
        }
    }
}
