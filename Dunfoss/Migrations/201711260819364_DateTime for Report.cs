namespace Dunfoss.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTimeforReport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reports", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reports", "Date");
        }
    }
}
