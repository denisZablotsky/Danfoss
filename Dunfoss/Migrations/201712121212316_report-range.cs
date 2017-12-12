namespace Dunfoss.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reportrange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reports", "from", c => c.Int(nullable: false));
            AddColumn("dbo.Reports", "to", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reports", "to");
            DropColumn("dbo.Reports", "from");
        }
    }
}
