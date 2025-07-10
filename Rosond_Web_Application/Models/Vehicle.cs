using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Rosond_Web_Application.Models
{
    public class Vehicle
    {//done
        [Key]
        public int VehicleId { get; set; }

        [Required]
        [StringLength(100)]
        public string Make { get; set; }

        [Required]
        [StringLength(100)]
        public string Model { get; set; }

        [Required]
        [StringLength(20)]
        [Index(IsUnique = true)]
        
        public string LicensePlate { get; set; }

        public int Year { get; set; }

       
        public int SupplierId { get; set; }
        public int BranchId { get; set; }
        public int ClientId { get; set; }
        public int DriverId { get; set; }

        
        [ForeignKey("SupplierId")]
        public Supplier Supplier { get; set; }

        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }

        [ForeignKey("DriverId")]
        public Driver Driver { get; set; }

    }
}