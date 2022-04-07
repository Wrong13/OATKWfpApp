namespace OATKWfpApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HWEfsd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Price");
        }
    }
}
