namespace pts2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update555 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.invoice_main", "pid", "dbo.proposal_info");
            DropIndex("dbo.invoice_main", new[] { "pid" });
            AlterColumn("dbo.invoice_main", "pid", c => c.Long());
            CreateIndex("dbo.invoice_main", "pid");
            AddForeignKey("dbo.invoice_main", "pid", "dbo.proposal_info", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.invoice_main", "pid", "dbo.proposal_info");
            DropIndex("dbo.invoice_main", new[] { "pid" });
            AlterColumn("dbo.invoice_main", "pid", c => c.Long(nullable: false));
            CreateIndex("dbo.invoice_main", "pid");
            AddForeignKey("dbo.invoice_main", "pid", "dbo.proposal_info", "Id", cascadeDelete: true);
        }
    }
}
