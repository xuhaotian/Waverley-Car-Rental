using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace Rental.Models
{
   
    public class Car
    {
        [Key]
        public int CarId { get; set; }
        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public double UnitPrice { get; set; }

        public int Quantity { get; set; }
        public int NumberofSeats { get; set; }
    }
}