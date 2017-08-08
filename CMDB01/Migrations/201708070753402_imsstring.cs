namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imsstring : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.accounts", "RequestIMS", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.accounts", "RequestIMS");
        }
    }
}
