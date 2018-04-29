namespace pts2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialschema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.attachments_info",
                c => new
                    {
                        cid = c.Long(nullable: false),
                        Id = c.Long(nullable: false, identity: true),
                        fileName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.enquiry_info", t => t.cid, cascadeDelete: true)
                .Index(t => t.cid);
            
            CreateTable(
                "dbo.enquiry_info",
                c => new
                    {
                        cid = c.Long(nullable: false),
                        Id = c.Long(nullable: false, identity: true),
                        projectname = c.String(),
                        source = c.Int(nullable: false),
                        remark1 = c.String(),
                        remark2 = c.String(),
                        remark3 = c.String(),
                        status = c.Int(nullable: false),
                        tag = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.client_info", t => t.cid, cascadeDelete: true)
                .Index(t => t.cid);
            
            CreateTable(
                "dbo.client_info",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        firstname = c.String(nullable: false),
                        lastname = c.String(),
                        companyname = c.String(nullable: false),
                        address1 = c.String(nullable: false, maxLength: 200),
                        address2 = c.String(maxLength: 200),
                        contact1 = c.String(nullable: false, maxLength: 10),
                        contact2 = c.String(maxLength: 10),
                        email_id = c.String(nullable: false),
                        vat_no = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
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
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.attachments_info", "cid", "dbo.enquiry_info");
            DropForeignKey("dbo.enquiry_info", "cid", "dbo.client_info");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.enquiry_info", new[] { "cid" });
            DropIndex("dbo.attachments_info", new[] { "cid" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.client_info");
            DropTable("dbo.enquiry_info");
            DropTable("dbo.attachments_info");
        }
    }
}
