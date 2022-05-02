namespace OATKWfpApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HistoryOrdersRemove : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HistoryOrders", "OrderId", "dbo.Orders");
            DropIndex("dbo.HistoryOrders", new[] { "OrderId" });

        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.HistoryOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Finnaly = c.DateTime(nullable: false),
                        OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.HistoryOrders", "OrderId");
            AddForeignKey("dbo.HistoryOrders", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
        }
    }
}
