namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contractContacts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.contracts", "contact_Id", "dbo.contacts");
            DropIndex("dbo.contracts", new[] { "contact_Id" });
            CreateTable(
                "dbo.contractcontacts",
                c => new
                    {
                        contract_Id = c.Int(nullable: false),
                        contact_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.contract_Id, t.contact_Id })
                .ForeignKey("dbo.contracts", t => t.contract_Id, cascadeDelete: true)
                .ForeignKey("dbo.contacts", t => t.contact_Id, cascadeDelete: true)
                .Index(t => t.contract_Id)
                .Index(t => t.contact_Id);
            
            DropColumn("dbo.contracts", "contact_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.contracts", "contact_Id", c => c.Int());
            DropForeignKey("dbo.contractcontacts", "contact_Id", "dbo.contacts");
            DropForeignKey("dbo.contractcontacts", "contract_Id", "dbo.contracts");
            DropIndex("dbo.contractcontacts", new[] { "contact_Id" });
            DropIndex("dbo.contractcontacts", new[] { "contract_Id" });
            DropTable("dbo.contractcontacts");
            CreateIndex("dbo.contracts", "contact_Id");
            AddForeignKey("dbo.contracts", "contact_Id", "dbo.contacts", "Id");
        }
    }
}
