namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class comments2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.comments", "entity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.comments", "entity", c => c.String());
        }
    }
}
