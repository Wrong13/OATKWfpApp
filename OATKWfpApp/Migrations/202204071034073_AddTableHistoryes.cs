namespace OATKWfpApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableHistoryes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "IsActual", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "Order_Id", c => c.Int());
            CreateIndex("dbo.Orders", "Order_Id");
            AddForeignKey("dbo.Orders", "Order_Id", "dbo.Orders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Order_Id", "dbo.Orders");
            DropIndex("dbo.Orders", new[] { "Order_Id" });
            DropColumn("dbo.Orders", "Order_Id");
            DropColumn("dbo.Orders", "IsActual");
        }
    }
}
