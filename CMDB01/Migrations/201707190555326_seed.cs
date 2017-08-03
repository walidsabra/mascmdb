namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seed : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UltimateId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        email = c.String(),
                        phone = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.contracts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ContractDate = c.String(),
                        Status = c.String(),
                        Opportunity = c.String(),
                        PCode = c.String(),
                        account_Id = c.Int(),
                        contact_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.accounts", t => t.account_Id)
                .ForeignKey("dbo.contacts", t => t.contact_Id)
                .Index(t => t.account_Id)
                .Index(t => t.contact_Id);
            
            CreateTable(
                "dbo.servers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DataCenter = c.String(),
                        DeployedVersion = c.String(),
                        FQDN = c.String(),
                        Role = c.String(),
                        contract_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.contracts", t => t.contract_Id)
                .Index(t => t.contract_Id);
            
            CreateTable(
                "dbo.datasources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        GUID = c.String(),
                        BeingMonitored = c.String(),
                        server_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.servers", t => t.server_Id)
                .Index(t => t.server_Id);
            
            CreateTable(
                "dbo.contactaccounts",
                c => new
                    {
                        contact_Id = c.Int(nullable: false),
                        account_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.contact_Id, t.account_Id })
                .ForeignKey("dbo.contacts", t => t.contact_Id, cascadeDelete: true)
                .ForeignKey("dbo.accounts", t => t.account_Id, cascadeDelete: true)
                .Index(t => t.contact_Id)
                .Index(t => t.account_Id);
            
            CreateTable(
                "dbo.servercontacts",
                c => new
                    {
                        server_Id = c.Int(nullable: false),
                        contact_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.server_Id, t.contact_Id })
                .ForeignKey("dbo.servers", t => t.server_Id, cascadeDelete: true)
                .ForeignKey("dbo.contacts", t => t.contact_Id, cascadeDelete: true)
                .Index(t => t.server_Id)
                .Index(t => t.contact_Id);
            
            CreateTable(
                "dbo.datasourcecontacts",
                c => new
                    {
                        datasource_Id = c.Int(nullable: false),
                        contact_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.datasource_Id, t.contact_Id })
                .ForeignKey("dbo.datasources", t => t.datasource_Id, cascadeDelete: true)
                .ForeignKey("dbo.contacts", t => t.contact_Id, cascadeDelete: true)
                .Index(t => t.datasource_Id)
                .Index(t => t.contact_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.contracts", "contact_Id", "dbo.contacts");
            DropForeignKey("dbo.datasources", "server_Id", "dbo.servers");
            DropForeignKey("dbo.datasourcecontacts", "contact_Id", "dbo.contacts");
            DropForeignKey("dbo.datasourcecontacts", "datasource_Id", "dbo.datasources");
            DropForeignKey("dbo.servers", "contract_Id", "dbo.contracts");
            DropForeignKey("dbo.servercontacts", "contact_Id", "dbo.contacts");
            DropForeignKey("dbo.servercontacts", "server_Id", "dbo.servers");
            DropForeignKey("dbo.contracts", "account_Id", "dbo.accounts");
            DropForeignKey("dbo.contactaccounts", "account_Id", "dbo.accounts");
            DropForeignKey("dbo.contactaccounts", "contact_Id", "dbo.contacts");
            DropIndex("dbo.datasourcecontacts", new[] { "contact_Id" });
            DropIndex("dbo.datasourcecontacts", new[] { "datasource_Id" });
            DropIndex("dbo.servercontacts", new[] { "contact_Id" });
            DropIndex("dbo.servercontacts", new[] { "server_Id" });
            DropIndex("dbo.contactaccounts", new[] { "account_Id" });
            DropIndex("dbo.contactaccounts", new[] { "contact_Id" });
            DropIndex("dbo.datasources", new[] { "server_Id" });
            DropIndex("dbo.servers", new[] { "contract_Id" });
            DropIndex("dbo.contracts", new[] { "contact_Id" });
            DropIndex("dbo.contracts", new[] { "account_Id" });
            DropTable("dbo.datasourcecontacts");
            DropTable("dbo.servercontacts");
            DropTable("dbo.contactaccounts");
            DropTable("dbo.datasources");
            DropTable("dbo.servers");
            DropTable("dbo.contracts");
            DropTable("dbo.contacts");
            DropTable("dbo.accounts");
        }
    }
}
