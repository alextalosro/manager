namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActionExecutionTime = c.DateTime(nullable: false),
                        Action = c.String(),
                        Reason = c.String(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        Password = c.String(),
                        UserRole = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LogItems", "User_Id", "dbo.Users");
            DropIndex("dbo.LogItems", new[] { "User_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.LogItems");
        }
    }
}
