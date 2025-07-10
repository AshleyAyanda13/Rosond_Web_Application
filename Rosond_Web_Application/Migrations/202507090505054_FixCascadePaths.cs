namespace Rosond_Web_Application.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixCascadePaths : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        BranchId = c.Int(nullable: false, identity: true),
                        BranchName = c.String(nullable: false, maxLength: 100),
                        Location = c.String(maxLength: 150),
                        ManagerName = c.String(maxLength: 100),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.BranchId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false, maxLength: 150),
                        ContactPerson = c.String(maxLength: 100),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        Address = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.ClientId);
            
            CreateTable(
                "dbo.Drivers",
                c => new
                    {
                        DriverId = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false, maxLength: 100),
                        LicenseNumber = c.String(nullable: false, maxLength: 20),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.DriverId);
            
            CreateTable(
                "dbo.Leases",
                c => new
                    {
                        LeaseId = c.Int(nullable: false, identity: true),
                        LeaseStartDate = c.DateTime(nullable: false),
                        LeaseEndDate = c.DateTime(nullable: false),
                        LeaseAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VehicleId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                        DriverId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LeaseId)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .ForeignKey("dbo.Drivers", t => t.DriverId)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId)
                .Index(t => t.VehicleId)
                .Index(t => t.ClientId)
                .Index(t => t.DriverId);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        VehicleId = c.Int(nullable: false, identity: true),
                        Make = c.String(nullable: false, maxLength: 100),
                        Model = c.String(nullable: false, maxLength: 100),
                        LicensePlate = c.String(nullable: false, maxLength: 20),
                        Year = c.Int(nullable: false),
                        SupplierId = c.Int(nullable: false),
                        BranchId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                        DriverId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VehicleId)
                .ForeignKey("dbo.Branches", t => t.BranchId, cascadeDelete: true)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .ForeignKey("dbo.Drivers", t => t.DriverId)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.SupplierId)
                .Index(t => t.BranchId)
                .Index(t => t.ClientId)
                .Index(t => t.DriverId);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SupplierId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        ContactPerson = c.String(maxLength: 200),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        Address = c.String(maxLength: 300),
                    })
                .PrimaryKey(t => t.SupplierId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leases", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.Vehicles", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.Vehicles", "DriverId", "dbo.Drivers");
            DropForeignKey("dbo.Vehicles", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Vehicles", "BranchId", "dbo.Branches");
            DropForeignKey("dbo.Leases", "DriverId", "dbo.Drivers");
            DropForeignKey("dbo.Leases", "ClientId", "dbo.Clients");
            DropIndex("dbo.Vehicles", new[] { "DriverId" });
            DropIndex("dbo.Vehicles", new[] { "ClientId" });
            DropIndex("dbo.Vehicles", new[] { "BranchId" });
            DropIndex("dbo.Vehicles", new[] { "SupplierId" });
            DropIndex("dbo.Leases", new[] { "DriverId" });
            DropIndex("dbo.Leases", new[] { "ClientId" });
            DropIndex("dbo.Leases", new[] { "VehicleId" });
            DropTable("dbo.Suppliers");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Leases");
            DropTable("dbo.Drivers");
            DropTable("dbo.Clients");
            DropTable("dbo.Branches");
        }
    }
}
