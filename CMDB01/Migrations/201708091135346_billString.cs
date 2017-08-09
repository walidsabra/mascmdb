namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class billString : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.accounts", "Billable", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.accounts", "Billable");
        }
    }
}
