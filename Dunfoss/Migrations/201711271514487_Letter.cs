namespace Dunfoss.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Letter : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Letters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Division = c.String(),
                        Table1 = c.String(),
                        Chart1 = c.String(),
                        Table2 = c.String(),
                        Table3 = c.String(),
                        Chart2 = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Letters");
        }
    }
}
