namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SLA : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.serverFarms", "SLA", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.serverFarms", "SLA");
        }
    }
}
