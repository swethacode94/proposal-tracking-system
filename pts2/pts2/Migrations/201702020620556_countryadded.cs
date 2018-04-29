namespace pts2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class countryadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.countries",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        countryname = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.countries");
        }
    }
}
