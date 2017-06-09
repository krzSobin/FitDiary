namespace FitDiary.SecuredApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Muscles : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MuscleInExcercises", "Excercise_Id1", "dbo.Excercises");
            DropForeignKey("dbo.MuscleInExcercises", "Excercise_Id2", "dbo.Excercises");
            DropIndex("dbo.MuscleInExcercises", new[] { "Excercise_Id1" });
            DropIndex("dbo.MuscleInExcercises", new[] { "Excercise_Id2" });
            AddColumn("dbo.MuscleInExcercises", "IsMainMuscle", c => c.Boolean(nullable: false));
            DropColumn("dbo.MuscleInExcercises", "Excercise_Id1");
            DropColumn("dbo.MuscleInExcercises", "Excercise_Id2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MuscleInExcercises", "Excercise_Id2", c => c.Int());
            AddColumn("dbo.MuscleInExcercises", "Excercise_Id1", c => c.Int());
            DropColumn("dbo.MuscleInExcercises", "IsMainMuscle");
            CreateIndex("dbo.MuscleInExcercises", "Excercise_Id2");
            CreateIndex("dbo.MuscleInExcercises", "Excercise_Id1");
            AddForeignKey("dbo.MuscleInExcercises", "Excercise_Id2", "dbo.Excercises", "Id");
            AddForeignKey("dbo.MuscleInExcercises", "Excercise_Id1", "dbo.Excercises", "Id");
        }
    }
}
