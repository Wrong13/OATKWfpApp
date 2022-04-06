namespace OATKWfpApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rep : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserRole", c => c.String());
            AlterColumn("dbo.Users", "UserName", c => c.String(maxLength: 100));
            AlterColumn("dbo.Users", "Login", c => c.String(maxLength: 20));
            AlterColumn("dbo.Users", "Password", c => c.String(maxLength: 15));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "Login", c => c.String());
            AlterColumn("dbo.Users", "UserName", c => c.String());
            DropColumn("dbo.Users", "UserRole");
        }
    }
}
