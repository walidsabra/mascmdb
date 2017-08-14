namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contactLinks : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.contactaccounts", "contact_Id", "dbo.contacts");
            DropForeignKey("dbo.contactaccounts", "account_Id", "dbo.accounts");
            DropIndex("dbo.contactaccounts", new[] { "contact_Id" });
            DropIndex("dbo.contactaccounts", new[] { "account_Id" });
            CreateTable(
                "dbo.ContactLinks",
                c => new
                    {
                        contactId = c.Int(nullable: false),
                        entityId = c.Int(nullable: false),
                        entityType = c.String(),
                        entityCategory = c.String(),
                        account_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.contactId, t.entityId })
                .ForeignKey("dbo.accounts", t => t.account_Id)
                .ForeignKey("dbo.contacts", t => t.contactId, cascadeDelete: true)
                .Index(t => t.contactId)
                .Index(t => t.account_Id);
            
            DropTable("dbo.contactaccounts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.contactaccounts",
                c => new
                    {
                        contact_Id = c.Int(nullable: false),
                        account_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.contact_Id, t.account_Id });
            
            DropForeignKey("dbo.ContactLinks", "contactId", "dbo.contacts");
            DropForeignKey("dbo.ContactLinks", "account_Id", "dbo.accounts");
            DropIndex("dbo.ContactLinks", new[] { "account_Id" });
            DropIndex("dbo.ContactLinks", new[] { "contactId" });
            DropTable("dbo.ContactLinks");
            CreateIndex("dbo.contactaccounts", "account_Id");
            CreateIndex("dbo.contactaccounts", "contact_Id");
            AddForeignKey("dbo.contactaccounts", "account_Id", "dbo.accounts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.contactaccounts", "contact_Id", "dbo.contacts", "Id", cascadeDelete: true);
        }
    }
}
