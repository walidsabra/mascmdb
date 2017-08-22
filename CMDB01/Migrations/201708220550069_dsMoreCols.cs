namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dsMoreCols : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.datasources", name: "server_Id", newName: "ServerFarm_Id");
            RenameIndex(table: "dbo.datasources", name: "IX_server_Id", newName: "IX_ServerFarm_Id");
            AddColumn("dbo.datasources", "Monitored", c => c.String());
            AddColumn("dbo.datasources", "ActiveUsers120Days", c => c.Int(nullable: false));
            AddColumn("dbo.datasources", "ActiveUsers30Days", c => c.Int(nullable: false));
            AddColumn("dbo.datasources", "Activeusers7Days", c => c.Int(nullable: false));
            AddColumn("dbo.datasources", "ActiveUsers90Days", c => c.Int(nullable: false));
            AddColumn("dbo.datasources", "ADUS", c => c.String());
            AddColumn("dbo.datasources", "Migration", c => c.String());
            AddColumn("dbo.datasources", "FileStorageSpace", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.datasources", "PSS", c => c.String());
            AddColumn("dbo.datasources", "Purpose", c => c.String());
            DropColumn("dbo.datasources", "BeingMonitored");
        }
        
        public override void Down()
        {
            AddColumn("dbo.datasources", "BeingMonitored", c => c.String());
            DropColumn("dbo.datasources", "Purpose");
            DropColumn("dbo.datasources", "PSS");
            DropColumn("dbo.datasources", "FileStorageSpace");
            DropColumn("dbo.datasources", "Migration");
            DropColumn("dbo.datasources", "ADUS");
            DropColumn("dbo.datasources", "ActiveUsers90Days");
            DropColumn("dbo.datasources", "Activeusers7Days");
            DropColumn("dbo.datasources", "ActiveUsers30Days");
            DropColumn("dbo.datasources", "ActiveUsers120Days");
            DropColumn("dbo.datasources", "Monitored");
            RenameIndex(table: "dbo.datasources", name: "IX_ServerFarm_Id", newName: "IX_server_Id");
            RenameColumn(table: "dbo.datasources", name: "ServerFarm_Id", newName: "server_Id");
        }
    }
}
