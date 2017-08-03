namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class comments4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.comments", "user", c => c.String());
            AddColumn("dbo.comments", "timestamp", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.comments", "timestamp");
            DropColumn("dbo.comments", "user");
        }
    }
}
