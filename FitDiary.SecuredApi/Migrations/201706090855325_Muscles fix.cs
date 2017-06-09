namespace FitDiary.SecuredApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Musclesfix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MuscleInExcercises", "Excercise_Id", "dbo.Excercises");
            DropIndex("dbo.MuscleInExcercises", new[] { "Excercise_Id" });
            RenameColumn(table: "dbo.MuscleInExcercises", name: "Excercise_Id", newName: "ExcerciseId");
            AlterColumn("dbo.MuscleInExcercises", "ExcerciseId", c => c.Int(nullable: false));
            CreateIndex("dbo.MuscleInExcercises", "ExcerciseId");
            AddForeignKey("dbo.MuscleInExcercises", "ExcerciseId", "dbo.Excercises", "Id", cascadeDelete: true);
            DropColumn("dbo.MuscleInExcercises", "ExcericseId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MuscleInExcercises", "ExcericseId", c => c.Int(nullable: false));
            DropForeignKey("dbo.MuscleInExcercises", "ExcerciseId", "dbo.Excercises");
            DropIndex("dbo.MuscleInExcercises", new[] { "ExcerciseId" });
            AlterColumn("dbo.MuscleInExcercises", "ExcerciseId", c => c.Int());
            RenameColumn(table: "dbo.MuscleInExcercises", name: "ExcerciseId", newName: "Excercise_Id");
            CreateIndex("dbo.MuscleInExcercises", "Excercise_Id");
            AddForeignKey("dbo.MuscleInExcercises", "Excercise_Id", "dbo.Excercises", "Id");
        }
    }
}
