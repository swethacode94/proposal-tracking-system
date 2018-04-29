namespace pts2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatein : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.invoice_item", "item", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.invoice_item", "item", c => c.String());
        }
    }
}
