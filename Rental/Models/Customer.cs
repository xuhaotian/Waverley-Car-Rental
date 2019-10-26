using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Rental.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string Role { get; set; }

        public string Email { get; set; }
        public string password { get; set; }
        public string Mobile { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }   

        public string UserId { get; set; }

    }
}