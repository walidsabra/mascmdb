namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class comments1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.comments", "account_Id", c => c.Int());
            AddColumn("dbo.comments", "datasource_Id", c => c.Int());
            CreateIndex("dbo.comments", "account_Id");
            CreateIndex("dbo.comments", "datasource_Id");
            AddForeignKey("dbo.comments", "account_Id", "dbo.accounts", "Id");
            AddForeignKey("dbo.comments", "datasource_Id", "dbo.datasources", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.comments", "datasource_Id", "dbo.datasources");
            DropForeignKey("dbo.comments", "account_Id", "dbo.accounts");
            DropIndex("dbo.comments", new[] { "datasource_Id" });
            DropIndex("dbo.comments", new[] { "account_Id" });
            DropColumn("dbo.comments", "datasource_Id");
            DropColumn("dbo.comments", "account_Id");
        }
    }
}
