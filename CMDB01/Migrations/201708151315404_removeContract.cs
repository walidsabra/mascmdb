namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeContract : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.contracts", "account_Id", "dbo.accounts");
            DropForeignKey("dbo.contractcontacts", "contract_Id", "dbo.contracts");
            DropForeignKey("dbo.contractcontacts", "contact_Id", "dbo.contacts");
            DropForeignKey("dbo.servers", "contract_Id", "dbo.contracts");
            DropIndex("dbo.contracts", new[] { "account_Id" });
            DropIndex("dbo.servers", new[] { "contract_Id" });
            DropIndex("dbo.contractcontacts", new[] { "contract_Id" });
            DropIndex("dbo.contractcontacts", new[] { "contact_Id" });
            DropColumn("dbo.servers", "contract_Id");
            DropTable("dbo.contracts");
            DropTable("dbo.contractcontacts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.contractcontacts",
                c => new
                    {
                        contract_Id = c.Int(nullable: false),
                        contact_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.contract_Id, t.contact_Id });
            
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
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.servers", "contract_Id", c => c.Int());
            CreateIndex("dbo.contractcontacts", "contact_Id");
            CreateIndex("dbo.contractcontacts", "contract_Id");
            CreateIndex("dbo.servers", "contract_Id");
            CreateIndex("dbo.contracts", "account_Id");
            AddForeignKey("dbo.servers", "contract_Id", "dbo.contracts", "Id");
            AddForeignKey("dbo.contractcontacts", "contact_Id", "dbo.contacts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.contractcontacts", "contract_Id", "dbo.contracts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.contracts", "account_Id", "dbo.accounts", "Id");
        }
    }
}
