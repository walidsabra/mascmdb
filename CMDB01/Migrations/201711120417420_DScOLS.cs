namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DScOLS : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.datasources", "ADUS");
            DropColumn("dbo.datasources", "Migration");
            DropColumn("dbo.datasources", "PSS");
        }
        
        public override void Down()
        {
            AddColumn("dbo.datasources", "PSS", c => c.String());
            AddColumn("dbo.datasources", "Migration", c => c.String());
            AddColumn("dbo.datasources", "ADUS", c => c.String());
        }
    }
}
