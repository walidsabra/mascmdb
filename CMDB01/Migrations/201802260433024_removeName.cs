namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.serverFarms", "FQDN", c => c.String(nullable: false));
            DropColumn("dbo.serverFarms", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.serverFarms", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.serverFarms", "FQDN", c => c.String());
        }
    }
}
