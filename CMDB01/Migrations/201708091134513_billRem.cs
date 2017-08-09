namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class billRem : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.accounts", "Billable");
        }
        
        public override void Down()
        {
            AddColumn("dbo.accounts", "Billable", c => c.Boolean(nullable: false));
        }
    }
}
