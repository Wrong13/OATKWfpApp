namespace OATKWfpApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPackOrder : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PackOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        IsPack = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PackOrders", "OrderId", "dbo.Orders");
            DropIndex("dbo.PackOrders", new[] { "OrderId" });
            DropTable("dbo.PackOrders");
        }
    }
}
