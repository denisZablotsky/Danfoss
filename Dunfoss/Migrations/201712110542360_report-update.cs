namespace Dunfoss.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reportupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reports", "image72", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reports", "image72");
        }
    }
}
