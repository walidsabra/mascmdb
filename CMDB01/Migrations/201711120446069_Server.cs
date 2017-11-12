namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Server : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Servers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ServerFarm_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.serverFarms", t => t.ServerFarm_Id)
                .Index(t => t.ServerFarm_Id);
            
            AddColumn("dbo.serverFarms", "DeployId", c => c.String());
            AddColumn("dbo.serverFarms", "MonitoringLink", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Servers", "ServerFarm_Id", "dbo.serverFarms");
            DropIndex("dbo.Servers", new[] { "ServerFarm_Id" });
            DropColumn("dbo.serverFarms", "MonitoringLink");
            DropColumn("dbo.serverFarms", "DeployId");
            DropTable("dbo.Servers");
        }
    }
}
