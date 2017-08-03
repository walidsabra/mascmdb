namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class comments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        entity = c.String(),
                        Comment = c.String(),
                        server_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.servers", t => t.server_Id)
                .Index(t => t.server_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.comments", "server_Id", "dbo.servers");
            DropIndex("dbo.comments", new[] { "server_Id" });
            DropTable("dbo.comments");
        }
    }
}
