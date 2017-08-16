namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requiredFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.accounts", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.contacts", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.datasources", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.servers", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.servers", "Name", c => c.String());
            AlterColumn("dbo.datasources", "Name", c => c.String());
            AlterColumn("dbo.contacts", "Name", c => c.String());
            AlterColumn("dbo.accounts", "Name", c => c.String());
        }
    }
}
