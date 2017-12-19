namespace Dunfoss.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class numbertypedivisions : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Letters", "Division", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Letters", "Division", c => c.String());
        }
    }
}
