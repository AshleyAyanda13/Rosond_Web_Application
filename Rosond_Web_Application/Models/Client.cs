using System;

using System.ComponentModel.DataAnnotations;


namespace Rosond_Web_Application.Models
{
    public class Client
    {//done

        [Key]
        public int ClientId { get; set; }

        [Required]
        [StringLength(150)]
        public string CompanyName { get; set; }

        [StringLength(100)]
        public string ContactPerson { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(250)]
        public string Address { get; set; }
    }
}