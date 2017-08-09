namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class roleOfferingPurpose : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.servers", "Offering", c => c.String());
            AddColumn("dbo.servers", "Purpose", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.servers", "Purpose");
            DropColumn("dbo.servers", "Offering");
        }
    }
}
