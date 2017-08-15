namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class servercontactLinks : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.servercontacts", "server_Id", "dbo.servers");
            DropForeignKey("dbo.servercontacts", "contact_Id", "dbo.contacts");
            DropIndex("dbo.servercontacts", new[] { "server_Id" });
            DropIndex("dbo.servercontacts", new[] { "contact_Id" });
            AddColumn("dbo.ContactLinks", "server_Id", c => c.Int());
            AddColumn("dbo.servers", "contact_Id", c => c.Int());
            CreateIndex("dbo.ContactLinks", "server_Id");
            CreateIndex("dbo.servers", "contact_Id");
            AddForeignKey("dbo.ContactLinks", "server_Id", "dbo.servers", "Id");
            AddForeignKey("dbo.servers", "contact_Id", "dbo.contacts", "Id");
            DropTable("dbo.servercontacts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.servercontacts",
                c => new
                    {
                        server_Id = c.Int(nullable: false),
                        contact_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.server_Id, t.contact_Id });
            
            DropForeignKey("dbo.servers", "contact_Id", "dbo.contacts");
            DropForeignKey("dbo.ContactLinks", "server_Id", "dbo.servers");
            DropIndex("dbo.servers", new[] { "contact_Id" });
            DropIndex("dbo.ContactLinks", new[] { "server_Id" });
            DropColumn("dbo.servers", "contact_Id");
            DropColumn("dbo.ContactLinks", "server_Id");
            CreateIndex("dbo.servercontacts", "contact_Id");
            CreateIndex("dbo.servercontacts", "server_Id");
            AddForeignKey("dbo.servercontacts", "contact_Id", "dbo.contacts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.servercontacts", "server_Id", "dbo.servers", "Id", cascadeDelete: true);
        }
    }
}
