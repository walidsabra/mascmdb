namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commentsLinkrem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.comments", "account_Id", "dbo.accounts");
            DropIndex("dbo.comments", new[] { "account_Id" });
            DropColumn("dbo.comments", "account_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.comments", "account_Id", c => c.Int());
            CreateIndex("dbo.comments", "account_Id");
            AddForeignKey("dbo.comments", "account_Id", "dbo.accounts", "Id");
        }
    }
}
