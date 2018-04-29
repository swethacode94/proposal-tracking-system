namespace pts2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialschema2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.client_info", "industry", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.client_info", "industry");
        }
    }
}
