namespace pts2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class industryadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.industries",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        industryname = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.industries");
        }
    }
}
