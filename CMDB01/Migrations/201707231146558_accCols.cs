namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class accCols : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.accounts", "RequestIMS", c => c.Boolean(nullable: false));
            AddColumn("dbo.accounts", "SuccessAdminLevel", c => c.String());
            AddColumn("dbo.accounts", "LinktoC4S", c => c.String());
            AddColumn("dbo.accounts", "Billable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.accounts", "Billable");
            DropColumn("dbo.accounts", "LinktoC4S");
            DropColumn("dbo.accounts", "SuccessAdminLevel");
            DropColumn("dbo.accounts", "RequestIMS");
        }
    }
}
