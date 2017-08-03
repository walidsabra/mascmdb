namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class serverAcc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.accounts", "ContractDate", c => c.String());
            AddColumn("dbo.accounts", "Status", c => c.String());
            AddColumn("dbo.accounts", "Opportunity", c => c.String());
            AddColumn("dbo.accounts", "ProjectorProject", c => c.String());
            AddColumn("dbo.servers", "account_Id", c => c.Int());
            CreateIndex("dbo.servers", "account_Id");
            AddForeignKey("dbo.servers", "account_Id", "dbo.accounts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.servers", "account_Id", "dbo.accounts");
            DropIndex("dbo.servers", new[] { "account_Id" });
            DropColumn("dbo.servers", "account_Id");
            DropColumn("dbo.accounts", "ProjectorProject");
            DropColumn("dbo.accounts", "Opportunity");
            DropColumn("dbo.accounts", "Status");
            DropColumn("dbo.accounts", "ContractDate");
        }
    }
}
