namespace OATKWfpApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Step4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 100),
                        Login = c.String(maxLength: 20),
                        Password = c.String(maxLength: 15),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
        }
    }
}
