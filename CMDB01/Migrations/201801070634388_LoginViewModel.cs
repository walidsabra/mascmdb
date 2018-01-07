namespace CMDB01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LoginViewModel : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.LoginViewModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LoginViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
