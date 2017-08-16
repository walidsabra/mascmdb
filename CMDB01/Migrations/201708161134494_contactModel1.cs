namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contactModel1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.accounts", "contact_Id", "dbo.contacts");
            DropForeignKey("dbo.datasources", "contact_Id", "dbo.contacts");
            DropForeignKey("dbo.servers", "contact_Id", "dbo.contacts");
            DropIndex("dbo.accounts", new[] { "contact_Id" });
            DropIndex("dbo.datasources", new[] { "contact_Id" });
            DropIndex("dbo.servers", new[] { "contact_Id" });
            DropColumn("dbo.accounts", "contact_Id");
            DropColumn("dbo.datasources", "contact_Id");
            DropColumn("dbo.servers", "contact_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.servers", "contact_Id", c => c.Int());
            AddColumn("dbo.datasources", "contact_Id", c => c.Int());
            AddColumn("dbo.accounts", "contact_Id", c => c.Int());
            CreateIndex("dbo.servers", "contact_Id");
            CreateIndex("dbo.datasources", "contact_Id");
            CreateIndex("dbo.accounts", "contact_Id");
            AddForeignKey("dbo.servers", "contact_Id", "dbo.contacts", "Id");
            AddForeignKey("dbo.datasources", "contact_Id", "dbo.contacts", "Id");
            AddForeignKey("dbo.accounts", "contact_Id", "dbo.contacts", "Id");
        }
    }
}
