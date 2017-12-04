namespace Dunfoss.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReportMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        image1 = c.String(),
                        image2 = c.String(),
                        image3 = c.String(),
                        image4 = c.String(),
                        image5 = c.String(),
                        image6 = c.String(),
                        image7 = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Reports");
        }
    }
}
