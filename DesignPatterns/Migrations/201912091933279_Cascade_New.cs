namespace DesignPatterns.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cascade_New : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        answer = c.String(),
                        IsTrue = c.Boolean(nullable: false),
                        question_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.question_Id)
                .Index(t => t.question_Id);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        question = c.String(),
                        Pattern_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patterns", t => t.Pattern_Id)
                .Index(t => t.Pattern_Id);
            
            CreateTable(
                "dbo.Patterns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        type = c.Int(nullable: false),
                        pattern_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patterns", t => t.pattern_Id)
                .Index(t => t.pattern_Id);
            
            CreateTable(
                "dbo.Marks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        mark = c.Int(nullable: false),
                        percent = c.Int(nullable: false),
                        difficulty = c.Int(nullable: false),
                        pattern_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patterns", t => t.pattern_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.pattern_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.MethodParameters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        method_Id = c.Int(),
                        type_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubjectMethods", t => t.method_Id)
                .ForeignKey("dbo.Subjects", t => t.type_Id)
                .Index(t => t.method_Id)
                .Index(t => t.type_Id);
            
            CreateTable(
                "dbo.SubjectMethods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AccessType = c.Int(nullable: false),
                        ReturnValue_Id = c.Int(),
                        Subject_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.ReturnValue_Id)
                .ForeignKey("dbo.Subjects", t => t.Subject_Id)
                .Index(t => t.ReturnValue_Id)
                .Index(t => t.Subject_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.SubjectProperties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Subject_Id = c.Int(),
                        Type_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.Subject_Id)
                .ForeignKey("dbo.Subjects", t => t.Type_Id)
                .Index(t => t.Subject_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.SubjectReferences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        type = c.Int(nullable: false),
                        subject_Id = c.Int(),
                        target_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.subject_Id)
                .ForeignKey("dbo.Subjects", t => t.target_Id)
                .Index(t => t.subject_Id)
                .Index(t => t.target_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubjectReferences", "target_Id", "dbo.Subjects");
            DropForeignKey("dbo.SubjectReferences", "subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.SubjectProperties", "Type_Id", "dbo.Subjects");
            DropForeignKey("dbo.SubjectProperties", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.MethodParameters", "type_Id", "dbo.Subjects");
            DropForeignKey("dbo.SubjectMethods", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.SubjectMethods", "ReturnValue_Id", "dbo.Subjects");
            DropForeignKey("dbo.MethodParameters", "method_Id", "dbo.SubjectMethods");
            DropForeignKey("dbo.Marks", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Marks", "pattern_Id", "dbo.Patterns");
            DropForeignKey("dbo.Questions", "Pattern_Id", "dbo.Patterns");
            DropForeignKey("dbo.Subjects", "pattern_Id", "dbo.Patterns");
            DropForeignKey("dbo.Answers", "question_Id", "dbo.Questions");
            DropIndex("dbo.SubjectReferences", new[] { "target_Id" });
            DropIndex("dbo.SubjectReferences", new[] { "subject_Id" });
            DropIndex("dbo.SubjectProperties", new[] { "Type_Id" });
            DropIndex("dbo.SubjectProperties", new[] { "Subject_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.SubjectMethods", new[] { "Subject_Id" });
            DropIndex("dbo.SubjectMethods", new[] { "ReturnValue_Id" });
            DropIndex("dbo.MethodParameters", new[] { "type_Id" });
            DropIndex("dbo.MethodParameters", new[] { "method_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Marks", new[] { "User_Id" });
            DropIndex("dbo.Marks", new[] { "pattern_Id" });
            DropIndex("dbo.Subjects", new[] { "pattern_Id" });
            DropIndex("dbo.Questions", new[] { "Pattern_Id" });
            DropIndex("dbo.Answers", new[] { "question_Id" });
            DropTable("dbo.SubjectReferences");
            DropTable("dbo.SubjectProperties");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.SubjectMethods");
            DropTable("dbo.MethodParameters");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Marks");
            DropTable("dbo.Subjects");
            DropTable("dbo.Patterns");
            DropTable("dbo.Questions");
            DropTable("dbo.Answers");
        }
    }
}
