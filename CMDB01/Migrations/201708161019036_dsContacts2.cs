namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dsContacts2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.datasources", "contact_Id", "dbo.contacts");
            DropForeignKey("dbo.servers", "contact_Id", "dbo.contacts");
            DropIndex("dbo.datasources", new[] { "contact_Id" });
            DropIndex("dbo.servers", new[] { "contact_Id" });
            DropColumn("dbo.datasources", "contact_Id");
            DropColumn("dbo.servers", "contact_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.servers", "contact_Id", c => c.Int());
            AddColumn("dbo.datasources", "contact_Id", c => c.Int());
            CreateIndex("dbo.servers", "contact_Id");
            CreateIndex("dbo.datasources", "contact_Id");
            AddForeignKey("dbo.servers", "contact_Id", "dbo.contacts", "Id");
            AddForeignKey("dbo.datasources", "contact_Id", "dbo.contacts", "Id");
        }
    }
}
