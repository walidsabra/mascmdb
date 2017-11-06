namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SFStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.serverFarms", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.serverFarms", "Status");
        }
    }
}
