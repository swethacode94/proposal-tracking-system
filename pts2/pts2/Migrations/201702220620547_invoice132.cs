namespace pts2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoice132 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.invoice_item",
                c => new
                    {
                        imid = c.Long(nullable: false),
                        Id = c.Long(nullable: false, identity: true),
                        item = c.String(),
                        quantity = c.Int(nullable: false),
                        rate = c.Single(nullable: false),
                        total = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.invoice_main", t => t.imid, cascadeDelete: true)
                .Index(t => t.imid);
            
            CreateTable(
                "dbo.invoice_main",
                c => new
                    {
                        pid = c.Long(nullable: false),
                        Id = c.Long(nullable: false, identity: true),
                        proposal_name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.proposal_info", t => t.pid, cascadeDelete: true)
                .Index(t => t.pid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.invoice_item", "imid", "dbo.invoice_main");
            DropForeignKey("dbo.invoice_main", "pid", "dbo.proposal_info");
            DropIndex("dbo.invoice_main", new[] { "pid" });
            DropIndex("dbo.invoice_item", new[] { "imid" });
            DropTable("dbo.invoice_main");
            DropTable("dbo.invoice_item");
        }
    }
}
