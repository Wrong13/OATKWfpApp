namespace OATKWfpApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HWE : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "IsBuy", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "Created", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Created");
            DropColumn("dbo.Orders", "IsBuy");
        }
    }
}
