namespace pts2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialschema4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.enquiry_info", "projectname", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.enquiry_info", "projectname", c => c.String());
        }
    }
}
