namespace DesignPatterns.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubjectMethods", "Subject_Id", c => c.Int());
            AddColumn("dbo.SubjectProperties", "Subject_Id", c => c.Int());
            CreateIndex("dbo.SubjectMethods", "Subject_Id");
            CreateIndex("dbo.SubjectProperties", "Subject_Id");
            AddForeignKey("dbo.SubjectMethods", "Subject_Id", "dbo.Subjects", "Id");
            AddForeignKey("dbo.SubjectProperties", "Subject_Id", "dbo.Subjects", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubjectProperties", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.SubjectMethods", "Subject_Id", "dbo.Subjects");
            DropIndex("dbo.SubjectProperties", new[] { "Subject_Id" });
            DropIndex("dbo.SubjectMethods", new[] { "Subject_Id" });
            DropColumn("dbo.SubjectProperties", "Subject_Id");
            DropColumn("dbo.SubjectMethods", "Subject_Id");
        }
    }
}
