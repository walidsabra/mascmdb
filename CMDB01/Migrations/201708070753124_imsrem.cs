namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imsrem : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.accounts", "RequestIMS");
        }
        
        public override void Down()
        {
            AddColumn("dbo.accounts", "RequestIMS", c => c.Boolean(nullable: false));
        }
    }
}
