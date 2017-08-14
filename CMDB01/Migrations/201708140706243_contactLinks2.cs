namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contactLinks2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ContactLinks", "contactId", "dbo.contacts");
            DropIndex("dbo.ContactLinks", new[] { "contactId" });
            DropPrimaryKey("dbo.ContactLinks");
            AddColumn("dbo.ContactLinks", "contact_Id", c => c.Int());
            AlterColumn("dbo.ContactLinks", "contactId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ContactLinks", "contactId");
            CreateIndex("dbo.ContactLinks", "contact_Id");
            AddForeignKey("dbo.ContactLinks", "contact_Id", "dbo.contacts", "Id");
            DropColumn("dbo.ContactLinks", "entityId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContactLinks", "entityId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ContactLinks", "contact_Id", "dbo.contacts");
            DropIndex("dbo.ContactLinks", new[] { "contact_Id" });
            DropPrimaryKey("dbo.ContactLinks");
            AlterColumn("dbo.ContactLinks", "contactId", c => c.Int(nullable: false));
            DropColumn("dbo.ContactLinks", "contact_Id");
            AddPrimaryKey("dbo.ContactLinks", new[] { "contactId", "entityId" });
            CreateIndex("dbo.ContactLinks", "contactId");
            AddForeignKey("dbo.ContactLinks", "contactId", "dbo.contacts", "Id", cascadeDelete: true);
        }
    }
}
