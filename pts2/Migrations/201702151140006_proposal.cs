namespace pts2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class proposal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.proposal_info",
                c => new
                    {
                        eid = c.Long(nullable: false),
                        Id = c.Long(nullable: false, identity: true),
                        prop_enquiry = c.String(),
                        sentdate = c.DateTime(nullable: false),
                        sent_to = c.String(),
                        sent_via = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.enquiry_info", t => t.eid, cascadeDelete: true)
                .Index(t => t.eid);
            
            CreateTable(
                "dbo.pros_attachments_info",
                c => new
                    {
                        pid = c.Long(nullable: false),
                        Id = c.Long(nullable: false, identity: true),
                        fileName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.proposal_info", t => t.pid, cascadeDelete: true)
                .Index(t => t.pid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.pros_attachments_info", "pid", "dbo.proposal_info");
            DropForeignKey("dbo.proposal_info", "eid", "dbo.enquiry_info");
            DropIndex("dbo.pros_attachments_info", new[] { "pid" });
            DropIndex("dbo.proposal_info", new[] { "eid" });
            DropTable("dbo.pros_attachments_info");
            DropTable("dbo.proposal_info");
        }
    }
}
