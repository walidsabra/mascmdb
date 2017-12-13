namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newSFFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.serverFarms", "Role", c => c.String());
            AddColumn("dbo.serverFarms", "DeploymentId", c => c.String());
            AddColumn("dbo.serverFarms", "AdminServerFQDN", c => c.String());
            AddColumn("dbo.serverFarms", "SQLServerFQDN", c => c.String());
            AddColumn("dbo.serverFarms", "SQLServerFQDNMirror", c => c.String());
            AddColumn("dbo.serverFarms", "Monitored", c => c.String());
            AddColumn("dbo.serverFarms", "Provider", c => c.String());
            DropColumn("dbo.serverFarms", "Purpose");
            DropColumn("dbo.serverFarms", "DeployId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.serverFarms", "DeployId", c => c.String());
            AddColumn("dbo.serverFarms", "Purpose", c => c.String());
            DropColumn("dbo.serverFarms", "Provider");
            DropColumn("dbo.serverFarms", "Monitored");
            DropColumn("dbo.serverFarms", "SQLServerFQDNMirror");
            DropColumn("dbo.serverFarms", "SQLServerFQDN");
            DropColumn("dbo.serverFarms", "AdminServerFQDN");
            DropColumn("dbo.serverFarms", "DeploymentId");
            DropColumn("dbo.serverFarms", "Role");
        }
    }
}
