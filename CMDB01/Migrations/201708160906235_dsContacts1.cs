namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dsContacts1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.datasourcecontacts", "datasource_Id", "dbo.datasources");
            DropForeignKey("dbo.datasourcecontacts", "contact_Id", "dbo.contacts");
            DropIndex("dbo.datasourcecontacts", new[] { "datasource_Id" });
            DropIndex("dbo.datasourcecontacts", new[] { "contact_Id" });
            AddColumn("dbo.ContactLinks", "datasource_Id", c => c.Int());
            AddColumn("dbo.datasources", "contact_Id", c => c.Int());
            CreateIndex("dbo.ContactLinks", "datasource_Id");
            CreateIndex("dbo.datasources", "contact_Id");
            AddForeignKey("dbo.ContactLinks", "datasource_Id", "dbo.datasources", "Id");
            AddForeignKey("dbo.datasources", "contact_Id", "dbo.contacts", "Id");
            DropTable("dbo.datasourcecontacts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.datasourcecontacts",
                c => new
                    {
                        datasource_Id = c.Int(nullable: false),
                        contact_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.datasource_Id, t.contact_Id });
            
            DropForeignKey("dbo.datasources", "contact_Id", "dbo.contacts");
            DropForeignKey("dbo.ContactLinks", "datasource_Id", "dbo.datasources");
            DropIndex("dbo.datasources", new[] { "contact_Id" });
            DropIndex("dbo.ContactLinks", new[] { "datasource_Id" });
            DropColumn("dbo.datasources", "contact_Id");
            DropColumn("dbo.ContactLinks", "datasource_Id");
            CreateIndex("dbo.datasourcecontacts", "contact_Id");
            CreateIndex("dbo.datasourcecontacts", "datasource_Id");
            AddForeignKey("dbo.datasourcecontacts", "contact_Id", "dbo.contacts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.datasourcecontacts", "datasource_Id", "dbo.datasources", "Id", cascadeDelete: true);
        }
    }
}
