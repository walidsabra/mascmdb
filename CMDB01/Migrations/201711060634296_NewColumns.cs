namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.serverFarms", "FreeStorageSpaceMB", c => c.Int(nullable: false));
            DropColumn("dbo.serverFarms", "FreesStorageSpaceMB");
        }
        
        public override void Down()
        {
            AddColumn("dbo.serverFarms", "FreesStorageSpaceMB", c => c.Int(nullable: false));
            DropColumn("dbo.serverFarms", "FreeStorageSpaceMB");
        }
    }
}
