namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contactCompany : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.contacts", "company", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.contacts", "company");
        }
    }
}
