using Rosond_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Web;
namespace Rosond_Web_Application.Data
{
    public class MyDbContext:DbContext
    {

        public MyDbContext() : base("MyConnectionString") { }

        public DbSet<Branch> Branches { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Lease> Leases { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
             
            modelBuilder.Entity<Vehicle>()
                .HasRequired(v => v.Client)
                .WithMany()
                .HasForeignKey(v => v.ClientId)
                .WillCascadeOnDelete(false);

             
            modelBuilder.Entity<Vehicle>()
                .HasRequired(v => v.Driver)
                .WithMany()
                .HasForeignKey(v => v.DriverId)
                .WillCascadeOnDelete(false);

             
            modelBuilder.Entity<Lease>()
                .HasRequired(l => l.Client)
                .WithMany()
                .HasForeignKey(l => l.ClientId)
                .WillCascadeOnDelete(false);

            
            modelBuilder.Entity<Lease>()
                .HasRequired(l => l.Driver)
                .WithMany()
                .HasForeignKey(l => l.DriverId)
                .WillCascadeOnDelete(false);

            
            modelBuilder.Entity<Lease>()
                .HasRequired(l => l.Vehicle)
                .WithMany()
                .HasForeignKey(l => l.VehicleId)
                .WillCascadeOnDelete(false);
        }



    }

}