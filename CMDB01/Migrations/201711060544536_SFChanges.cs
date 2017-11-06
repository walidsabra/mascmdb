namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SFChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.serverFarms", "Architecture", c => c.String());
            AddColumn("dbo.serverFarms", "FreesStorageSpaceMB", c => c.Int(nullable: false));
            DropColumn("dbo.serverFarms", "Role");
            DropColumn("dbo.serverFarms", "Offering");
        }
        
        public override void Down()
        {
            AddColumn("dbo.serverFarms", "Offering", c => c.String());
            AddColumn("dbo.serverFarms", "Role", c => c.String());
            DropColumn("dbo.serverFarms", "FreesStorageSpaceMB");
            DropColumn("dbo.serverFarms", "Architecture");
        }
    }
}
