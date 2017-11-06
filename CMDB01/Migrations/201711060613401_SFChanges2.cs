namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SFChanges2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.comments", "user", c => c.String(nullable: false));
            AlterColumn("dbo.comments", "Comment", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.comments", "Comment", c => c.String());
            AlterColumn("dbo.comments", "user", c => c.String());
        }
    }
}
