using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rosond_Web_Application.Models
{
    public class Branch
    {//done
        [Key]
        public int BranchId { get; set; }

        [Required]
        [StringLength(100)]
        public string BranchName { get; set; }

        [StringLength(150)]
        public string Location { get; set; }

        [StringLength(100)]
        public string ManagerName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

    }

}