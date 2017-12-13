namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccountContractType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.accounts", "ContractType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.accounts", "ContractType");
        }
    }
}
