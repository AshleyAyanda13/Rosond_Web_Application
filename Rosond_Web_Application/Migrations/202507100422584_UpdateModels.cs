namespace Rosond_Web_Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModels : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Vehicles", "LicensePlate", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Vehicles", new[] { "LicensePlate" });
        }
    }
}
