namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contactModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.accounts", "contact_Id", c => c.Int());
            AddColumn("dbo.datasources", "contact_Id", c => c.Int());
            AddColumn("dbo.servers", "contact_Id", c => c.Int());
            CreateIndex("dbo.accounts", "contact_Id");
            CreateIndex("dbo.datasources", "contact_Id");
            CreateIndex("dbo.servers", "contact_Id");
            AddForeignKey("dbo.accounts", "contact_Id", "dbo.contacts", "Id");
            AddForeignKey("dbo.datasources", "contact_Id", "dbo.contacts", "Id");
            AddForeignKey("dbo.servers", "contact_Id", "dbo.contacts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.servers", "contact_Id", "dbo.contacts");
            DropForeignKey("dbo.datasources", "contact_Id", "dbo.contacts");
            DropForeignKey("dbo.accounts", "contact_Id", "dbo.contacts");
            DropIndex("dbo.servers", new[] { "contact_Id" });
            DropIndex("dbo.datasources", new[] { "contact_Id" });
            DropIndex("dbo.accounts", new[] { "contact_Id" });
            DropColumn("dbo.servers", "contact_Id");
            DropColumn("dbo.datasources", "contact_Id");
            DropColumn("dbo.accounts", "contact_Id");
        }
    }
}
