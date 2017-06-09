namespace FitDiary.SecuredApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Excerciseschangesv2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Excercises", "WorkoutId", "dbo.Workouts");
            DropIndex("dbo.Excercises", new[] { "WorkoutId" });
            RenameColumn(table: "dbo.Excercises", name: "WorkoutId", newName: "Workout_Id");
            AddColumn("dbo.ExcerciseSeries", "WorkoutId", c => c.Int(nullable: false));
            AlterColumn("dbo.Excercises", "Workout_Id", c => c.Int());
            CreateIndex("dbo.Excercises", "Workout_Id");
            CreateIndex("dbo.ExcerciseSeries", "WorkoutId");
            AddForeignKey("dbo.ExcerciseSeries", "WorkoutId", "dbo.Workouts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Excercises", "Workout_Id", "dbo.Workouts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Excercises", "Workout_Id", "dbo.Workouts");
            DropForeignKey("dbo.ExcerciseSeries", "WorkoutId", "dbo.Workouts");
            DropIndex("dbo.ExcerciseSeries", new[] { "WorkoutId" });
            DropIndex("dbo.Excercises", new[] { "Workout_Id" });
            AlterColumn("dbo.Excercises", "Workout_Id", c => c.Int(nullable: false));
            DropColumn("dbo.ExcerciseSeries", "WorkoutId");
            RenameColumn(table: "dbo.Excercises", name: "Workout_Id", newName: "WorkoutId");
            CreateIndex("dbo.Excercises", "WorkoutId");
            AddForeignKey("dbo.Excercises", "WorkoutId", "dbo.Workouts", "Id", cascadeDelete: true);
        }
    }
}
