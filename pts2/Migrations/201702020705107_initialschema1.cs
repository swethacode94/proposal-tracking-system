namespace pts2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialschema1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.client_info", "country", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.client_info", "country");
        }
    }
}
