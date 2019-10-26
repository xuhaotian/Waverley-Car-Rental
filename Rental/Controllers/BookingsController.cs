using Rental.Models;
using Rental.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web.Security;
using AuthorizeAttribute = System.Web.Mvc.AuthorizeAttribute;

namespace Rental.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {

        private ApplicationDbContext _context;


        public BookingsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Bookings
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var currentUserRole = _context.Customers.Where(u => u.UserId == userId).FirstOrDefault().Role;
            var bookings = _context.Bookings.Include(a => a.Car).Include(a => a.Customer);
            //Log-in customer can only see bookings that are made by themselves         
            if (currentUserRole == "Customer")
            {
                bookings = bookings.Where(a => a.Customer.UserId == userId);
            }

            return View(bookings.ToList());
        }

        public ActionResult New()
        {
            var booking = new BookingViewModel();
            //constraint: only allow user to select cars whose quantity are greater than 0
            var carList = _context.Cars.Where(c => c.Quantity > 0).ToList();
            ViewBag.carList = carList;
            return View("NewBooking", booking);
        }

        public ActionResult Save(BookingViewModel viewModel, string CarId)
        {
            var user = User.Identity.GetUserId();
            var carIdToInt = Int32.Parse(CarId.Replace("/", ""));
            var unitPrice = _context.Cars.Where(c => c.CarId == carIdToInt).FirstOrDefault().UnitPrice;
            if (viewModel.BookingId == 0)
            {
                _context.Bookings.Add(new Booking
                {
                    BookingId = viewModel.BookingId,
                    CarId = carIdToInt,
                    CustomerId = _context.Customers.Where(c => c.UserId == user).FirstOrDefault().CustomerId,
                    StartDate = viewModel.StartDate,
                    EndDate = viewModel.EndDate,
                    TotalPrice = unitPrice * (viewModel.EndDate.Subtract(viewModel.StartDate)).TotalDays
                });
                _context.Cars.Where(c => c.CarId == carIdToInt).FirstOrDefault().Quantity--;
            }
            else
            {
                var bookingInDb = _context.Bookings.FirstOrDefault(c => c.BookingId == viewModel.BookingId);
                bookingInDb.BookingId = viewModel.BookingId;
                bookingInDb.CarId = CarId == null ? bookingInDb.CarId : Int32.Parse(CarId.Replace("/", ""));
                bookingInDb.StartDate = viewModel.StartDate;
                bookingInDb.EndDate = viewModel.EndDate;
                bookingInDb.TotalPrice = viewModel.UnitPrice * (viewModel.EndDate.Subtract(viewModel.StartDate)).TotalDays;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Bookings");
        }

        public ActionResult Delete(int id)
        {
            var bookingInDb = _context.Bookings.SingleOrDefault(c => c.BookingId == id);

            if (bookingInDb != null)
            {
                _context.Bookings.Remove(bookingInDb);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Bookings");
        }

        public ActionResult Update(int id)
        {
            var carList = _context.Cars.Where(c => c.Quantity > 0).ToList();
            ViewBag.carList = carList;
            var booking = _context.Bookings.Include(c => c.Customer).Include(c => c.Car).SingleOrDefault(c => c.BookingId == id);
            var viewModel = new BookingViewModel
            {
                BookingId = booking.BookingId,
                CustomerId = booking.CustomerId,
                FirstName = booking.Customer.FirstName,
                LastName = booking.Customer.LastName,
                CarId = booking.CarId,
                CarMake = booking.Car.CarMake,
                CarModel = booking.Car.CarModel,
                UnitPrice = booking.Car.UnitPrice,
                Quantity = booking.Car.Quantity,
                NumberofSeats = booking.Car.NumberofSeats,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                TotalPrice = booking.TotalPrice
            };

            if (booking == null)
            {
                return HttpNotFound();
            }
            return View("NewBooking", viewModel);
        }
    }
}