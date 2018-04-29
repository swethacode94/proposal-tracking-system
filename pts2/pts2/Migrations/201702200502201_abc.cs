namespace pts2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abc : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.proposal_info", "eid", "dbo.enquiry_info");
            DropIndex("dbo.proposal_info", new[] { "eid" });
            AlterColumn("dbo.proposal_info", "eid", c => c.Long());
            CreateIndex("dbo.proposal_info", "eid");
            AddForeignKey("dbo.proposal_info", "eid", "dbo.enquiry_info", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.proposal_info", "eid", "dbo.enquiry_info");
            DropIndex("dbo.proposal_info", new[] { "eid" });
            AlterColumn("dbo.proposal_info", "eid", c => c.Long(nullable: false));
            CreateIndex("dbo.proposal_info", "eid");
            AddForeignKey("dbo.proposal_info", "eid", "dbo.enquiry_info", "Id", cascadeDelete: true);
        }
    }
}
