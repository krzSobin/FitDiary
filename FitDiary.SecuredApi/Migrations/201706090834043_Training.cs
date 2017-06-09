namespace FitDiary.SecuredApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Training : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MuscleInExcercises",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MuscleId = c.Int(nullable: false),
                        ExcericseId = c.Int(nullable: false),
                        Excercise_Id = c.Int(),
                        Excercise_Id1 = c.Int(),
                        Excercise_Id2 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Excercises", t => t.Excercise_Id)
                .ForeignKey("dbo.Muscles", t => t.MuscleId, cascadeDelete: true)
                .ForeignKey("dbo.Excercises", t => t.Excercise_Id1)
                .ForeignKey("dbo.Excercises", t => t.Excercise_Id2)
                .Index(t => t.MuscleId)
                .Index(t => t.Excercise_Id)
                .Index(t => t.Excercise_Id1)
                .Index(t => t.Excercise_Id2);
            
            CreateTable(
                "dbo.Muscles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.ExcerciseSeries", "ExcerciseId");
            AddForeignKey("dbo.ExcerciseSeries", "ExcerciseId", "dbo.Excercises", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExcerciseSeries", "ExcerciseId", "dbo.Excercises");
            DropForeignKey("dbo.MuscleInExcercises", "Excercise_Id2", "dbo.Excercises");
            DropForeignKey("dbo.MuscleInExcercises", "Excercise_Id1", "dbo.Excercises");
            DropForeignKey("dbo.MuscleInExcercises", "MuscleId", "dbo.Muscles");
            DropForeignKey("dbo.MuscleInExcercises", "Excercise_Id", "dbo.Excercises");
            DropIndex("dbo.ExcerciseSeries", new[] { "ExcerciseId" });
            DropIndex("dbo.MuscleInExcercises", new[] { "Excercise_Id2" });
            DropIndex("dbo.MuscleInExcercises", new[] { "Excercise_Id1" });
            DropIndex("dbo.MuscleInExcercises", new[] { "Excercise_Id" });
            DropIndex("dbo.MuscleInExcercises", new[] { "MuscleId" });
            DropTable("dbo.Muscles");
            DropTable("dbo.MuscleInExcercises");
        }
    }
}
