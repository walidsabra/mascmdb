namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DSStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.datasources", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.datasources", "Status");
        }
    }
}
