using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Rosond_Web_Application.Models
{
    public class Lease
    {//done
        [Key]
        public int LeaseId { get; set; }

        [Required]
        public DateTime LeaseStartDate { get; set; }

        [Required]
        public DateTime LeaseEndDate { get; set; }

        [Required]
        public decimal LeaseAmount { get; set; }

        // Foreign Keys
        public int VehicleId { get; set; }
        public int ClientId { get; set; }
        public int DriverId { get; set; }

        // Entity References (no virtual)
        [ForeignKey("VehicleId")]
        public Vehicle Vehicle { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }

        [ForeignKey("DriverId")]
        public Driver Driver { get; set; }
    }
}