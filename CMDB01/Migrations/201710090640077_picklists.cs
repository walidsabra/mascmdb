namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class picklists : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PickLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PickListName = c.String(),
                        PickListValue = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PickLists");
        }
    }
}
