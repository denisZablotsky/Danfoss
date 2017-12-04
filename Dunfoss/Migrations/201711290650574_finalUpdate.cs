namespace Dunfoss.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class finalUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CurrentFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path1 = c.String(),
                        Path2 = c.String(),
                        Path3 = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        Name = c.String(),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Letters", "month", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Letters", "month");
            DropTable("dbo.Files");
            DropTable("dbo.CurrentFiles");
        }
    }
}
