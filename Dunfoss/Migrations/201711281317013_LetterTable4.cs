namespace Dunfoss.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LetterTable4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Letters", "Table4", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Letters", "Table4");
        }
    }
}
