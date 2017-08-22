namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Servr2ServerFarm1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.servers", newName: "serverFarms");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.serverFarms", newName: "servers");
        }
    }
}
