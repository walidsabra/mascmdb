namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class comments3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.comments", "account_Id", "dbo.accounts");
            DropForeignKey("dbo.comments", "server_Id", "dbo.servers");
            DropForeignKey("dbo.comments", "datasource_Id", "dbo.datasources");
            DropIndex("dbo.comments", new[] { "account_Id" });
            DropIndex("dbo.comments", new[] { "server_Id" });
            DropIndex("dbo.comments", new[] { "datasource_Id" });
            AddColumn("dbo.comments", "entity_Id", c => c.Int(nullable: false));
            AddColumn("dbo.comments", "entity", c => c.String());
            DropColumn("dbo.comments", "account_Id");
            DropColumn("dbo.comments", "server_Id");
            DropColumn("dbo.comments", "datasource_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.comments", "datasource_Id", c => c.Int());
            AddColumn("dbo.comments", "server_Id", c => c.Int());
            AddColumn("dbo.comments", "account_Id", c => c.Int());
            DropColumn("dbo.comments", "entity");
            DropColumn("dbo.comments", "entity_Id");
            CreateIndex("dbo.comments", "datasource_Id");
            CreateIndex("dbo.comments", "server_Id");
            CreateIndex("dbo.comments", "account_Id");
            AddForeignKey("dbo.comments", "datasource_Id", "dbo.datasources", "Id");
            AddForeignKey("dbo.comments", "server_Id", "dbo.servers", "Id");
            AddForeignKey("dbo.comments", "account_Id", "dbo.accounts", "Id");
        }
    }
}
