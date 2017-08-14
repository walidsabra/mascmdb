namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contactLinks3 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ContactLinks");
            AddColumn("dbo.ContactLinks", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ContactLinks", "Id");
            DropColumn("dbo.ContactLinks", "contactId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContactLinks", "contactId", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.ContactLinks");
            DropColumn("dbo.ContactLinks", "Id");
            AddPrimaryKey("dbo.ContactLinks", "contactId");
        }
    }
}
