using Rental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rental.ViewModels
{
    public class BookingViewModel
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CarId { get; set; }
        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public double UnitPrice { get; set; }

        public int Quantity { get; set; }
        public int NumberofSeats { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalPrice { get; set; }
    }
}