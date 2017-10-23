namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commentURL : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.comments", "Link", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.comments", "Link");
        }
    }
}
