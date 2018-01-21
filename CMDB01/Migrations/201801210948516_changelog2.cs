namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changelog2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.changelogs", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.changelogs", "UserName");
        }
    }
}
