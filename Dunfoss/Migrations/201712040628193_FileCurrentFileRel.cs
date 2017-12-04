namespace Dunfoss.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FileCurrentFileRel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CurrentFiles", "FileId1", c => c.Int(nullable: false));
            AddColumn("dbo.CurrentFiles", "FileId2", c => c.Int(nullable: false));
            AddColumn("dbo.CurrentFiles", "FileId3", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CurrentFiles", "FileId3");
            DropColumn("dbo.CurrentFiles", "FileId2");
            DropColumn("dbo.CurrentFiles", "FileId1");
        }
    }
}
