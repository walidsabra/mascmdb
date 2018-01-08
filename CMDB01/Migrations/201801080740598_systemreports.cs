namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class systemreports : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SystemReports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReportName = c.String(),
                        ReportPath = c.String(),
                        ReportCategory = c.String(),
                        DS1Name = c.String(),
                        DS2Name = c.String(),
                        DS3Name = c.String(),
                        DS4Name = c.String(),
                        DS5Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SystemReports");
        }
    }
}
