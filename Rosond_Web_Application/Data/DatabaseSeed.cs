using Rosond_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rosond_Web_Application.Data
{
    public class DatabaseSeed
    {



        public static void Seed(MyDbContext context)
        {

            if (!context.Suppliers.Any(s => s.SupplierId == 1))
            {
                context.Suppliers.Add(new Supplier
                {
                    SupplierId = 1,
                    Name = "AutoHub",
                    ContactPerson = "Zanele Mokoena",
                    PhoneNumber = "0845681458",
                    Email = "Zanele@Autohub.com",
                    Address = "56 Zenzele Street, Alberton"
                });
            }

            if (!context.Suppliers.Any(s => s.SupplierId == 2))
            {
                context.Suppliers.Add(new Supplier
                {
                    SupplierId = 2,
                    Name = "MotorMate",
                    ContactPerson = "Thabo Dube",
                    PhoneNumber = "0834567890",
                    Email = "Thabo@motormate.co.za",
                    Address = "12 Bree Street, Sandton"
                });
            }

            
            if (!context.Branches.Any(b => b.BranchId == 1))
            {
                context.Branches.Add(new Branch
                {
                    BranchId = 1,
                    BranchName = "Brakpan Central",
                    Location = "Brakpan",
                    ManagerName = "Sipho",
                    PhoneNumber = "0115897787",
                    Email = "Info@BrakC.com"
                });
            }

            if (!context.Branches.Any(b => b.BranchId == 2))
            {
                context.Branches.Add(new Branch
                {
                    BranchId = 2,
                    BranchName = "Sandton HQ",
                    Location = "Sandton",
                    ManagerName = "Lerato",
                    PhoneNumber = "0112256789",
                    Email = "hq@sandtonbranch.co.za"
                });
            }

             
            if (!context.Clients.Any(c => c.ClientId == 1))
            {
                context.Clients.Add(new Client
                {
                    ClientId = 1,
                    CompanyName = "FleetX",
                    ContactPerson = "Bontle",
                    PhoneNumber = "0117794568",
                    Email = "info@fleetx.co.za",
                    Address = "Prune Avenue, Cape Town"
                });
            }

            if (!context.Clients.Any(c => c.ClientId == 2))
            {
                context.Clients.Add(new Client
                {
                    ClientId = 2,
                    CompanyName = "LogiCorp",
                    ContactPerson = "Karabo",
                    PhoneNumber = "0213368970",
                    Email = "karabo@logicorp.com",
                    Address = "14 Logistics Lane, Durban"
                });
            }

           
            if (!context.Drivers.Any(d => d.DriverId == 1))
            {
                context.Drivers.Add(new Driver
                {
                    DriverId = 1,
                    FullName = "Sibongile Dlamini",
                    LicenseNumber = "123456b",
                    PhoneNumber = "0800055555",
                    Email = "Sbongile@Lease.com"
                });
            }

            if (!context.Drivers.Any(d => d.DriverId == 2))
            {
                context.Drivers.Add(new Driver
                {
                    DriverId = 2,
                    FullName = "Njabulo Khumalo",
                    LicenseNumber = "789101c",
                    PhoneNumber = "0821234567",
                    Email = "Njabulo@FleetDrive.co.za"
                });
            }

             
            if (!context.Vehicles.Any(v => v.VehicleId == 1))
            {
                context.Vehicles.Add(new Vehicle
                {
                    VehicleId = 1,
                    Make = "BMW",
                    Model = "3 Series",
                    Year = 2019,
                    LicensePlate = "AWEP456GP",
                    SupplierId = 1,
                    BranchId = 1,
                    ClientId = 1,
                    DriverId = 1
                });
            }

            if (!context.Vehicles.Any(v => v.VehicleId == 2))
            {
                context.Vehicles.Add(new Vehicle
                {
                    VehicleId = 2,
                    Make = "Toyota",
                    Model = "Hilux",
                    Year = 2022,
                    LicensePlate = "BTR123GP",
                    SupplierId = 2,
                    BranchId = 2,
                    ClientId = 2,
                    DriverId = 2
                });
            }

            
            if (!context.Leases.Any(l => l.LeaseId == 1))
            {
                context.Leases.Add(new Lease
                {
                    LeaseId = 1,
                    VehicleId = 1,
                    ClientId = 1,
                    DriverId = 1,
                    LeaseStartDate = DateTime.Now,
                    LeaseEndDate = DateTime.Now.AddDays(3),
                    LeaseAmount = 200
                });
            }

            if (!context.Leases.Any(l => l.LeaseId == 2))
            {
                context.Leases.Add(new Lease
                {
                    LeaseId = 2,
                    VehicleId = 2,
                    ClientId = 2,
                    DriverId = 2,
                    LeaseStartDate = DateTime.Now.AddDays(1),
                    LeaseEndDate = DateTime.Now.AddDays(7),
                    LeaseAmount = 350
                });
            }

            context.SaveChanges();
        }
    }
}
