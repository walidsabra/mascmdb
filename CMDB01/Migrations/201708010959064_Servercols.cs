namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Servercols : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.servers", "NonTypicalArchitecture", c => c.String());
            AddColumn("dbo.servers", "CustomSLA", c => c.String());
            AddColumn("dbo.servers", "ActiveUsers7Days", c => c.Int(nullable: false));
            AddColumn("dbo.servers", "ActiveUsers30Days", c => c.Int(nullable: false));
            AddColumn("dbo.servers", "ActiveUsers90Days", c => c.Int(nullable: false));
            AddColumn("dbo.servers", "ActiveUsers120Days", c => c.Int(nullable: false));
            AddColumn("dbo.servers", "FileStorageSpace", c => c.Int(nullable: false));
            AddColumn("dbo.servers", "ProvisionedStorageSpace", c => c.Int(nullable: false));
            AddColumn("dbo.servers", "FreeStoragePercent", c => c.Int(nullable: false));
            AddColumn("dbo.servers", "ServerFarmIPAddress", c => c.String());
            AddColumn("dbo.servers", "WebServerURL", c => c.String());
            AddColumn("dbo.servers", "WebServiceGatewayURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.servers", "WebServiceGatewayURL");
            DropColumn("dbo.servers", "WebServerURL");
            DropColumn("dbo.servers", "ServerFarmIPAddress");
            DropColumn("dbo.servers", "FreeStoragePercent");
            DropColumn("dbo.servers", "ProvisionedStorageSpace");
            DropColumn("dbo.servers", "FileStorageSpace");
            DropColumn("dbo.servers", "ActiveUsers120Days");
            DropColumn("dbo.servers", "ActiveUsers90Days");
            DropColumn("dbo.servers", "ActiveUsers30Days");
            DropColumn("dbo.servers", "ActiveUsers7Days");
            DropColumn("dbo.servers", "CustomSLA");
            DropColumn("dbo.servers", "NonTypicalArchitecture");
        }
    }
}
