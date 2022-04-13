namespace OATKWfpApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Step5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserRoleID", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "UserRoleID");
            AddForeignKey("dbo.Users", "UserRoleID", "dbo.UserRoles", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserRoleID", "dbo.UserRoles");
            DropIndex("dbo.Users", new[] { "UserRoleID" });
            DropColumn("dbo.Users", "UserRoleID");
        }
    }
}
