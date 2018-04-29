namespace pts2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialschema3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.enquiry_info", "remark2");
            DropColumn("dbo.enquiry_info", "remark3");
        }
        
        public override void Down()
        {
            AddColumn("dbo.enquiry_info", "remark3", c => c.String());
            AddColumn("dbo.enquiry_info", "remark2", c => c.String());
        }
    }
}
