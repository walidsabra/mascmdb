namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeAccountFields : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.accounts", "ContractDate");
            DropColumn("dbo.accounts", "SuccessAdminLevel");
            DropColumn("dbo.accounts", "LinktoC4S");
        }
        
        public override void Down()
        {
            AddColumn("dbo.accounts", "LinktoC4S", c => c.String());
            AddColumn("dbo.accounts", "SuccessAdminLevel", c => c.String());
            AddColumn("dbo.accounts", "ContractDate", c => c.String());
        }
    }
}
