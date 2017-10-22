namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class extraCommentFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.comments", "Type", c => c.String());
            AddColumn("dbo.comments", "featured", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.comments", "featured");
            DropColumn("dbo.comments", "Type");
        }
    }
}
