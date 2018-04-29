namespace pts2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update1234 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.proposal_info", "sent_to", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.proposal_info", "sent_to", c => c.String());
        }
    }
}
