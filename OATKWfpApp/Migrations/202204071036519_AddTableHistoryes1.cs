namespace OATKWfpApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableHistoryes1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Order_Id", "dbo.Orders");
            DropIndex("dbo.Orders", new[] { "Order_Id" });
            CreateTable(
                "dbo.HistoryOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Finnaly = c.DateTime(nullable: false),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
            DropColumn("dbo.Orders", "Order_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Order_Id", c => c.Int());
            DropForeignKey("dbo.HistoryOrders", "OrderId", "dbo.Orders");
            DropIndex("dbo.HistoryOrders", new[] { "OrderId" });
            DropTable("dbo.HistoryOrders");
            CreateIndex("dbo.Orders", "Order_Id");
            AddForeignKey("dbo.Orders", "Order_Id", "dbo.Orders", "Id");
        }
    }
}
