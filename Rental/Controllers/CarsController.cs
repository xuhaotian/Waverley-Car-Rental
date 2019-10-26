using Rental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rental.Controllers
{
    public class CarsController : Controller
    {
        private ApplicationDbContext _context;


        public CarsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Cars
        public ActionResult Index()
        {
            var cars = _context.Cars.ToList();
            return View(cars);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult New()
        {
            var Car = new Car();
            return View("CarForm", Car);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Save(Car car)
        {
            if (ModelState.IsValid)
            {
                if (car.CarId == 0)
                {
                    _context.Cars.Add(car);
                }
                else
                {
                    var carInDb = _context.Cars.FirstOrDefault(c => c.CarId == car.CarId);
                    carInDb.CarMake = car.CarMake;
                    carInDb.CarModel = car.CarModel;
                    carInDb.UnitPrice = car.UnitPrice;
                    carInDb.Quantity = car.Quantity;
                    carInDb.NumberofSeats = car.NumberofSeats;
                }
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Cars");
        }
        // only admin allowed to delete cars
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var carInDb = _context.Cars.SingleOrDefault(c => c.CarId == id);

            if (carInDb != null)
            {
                _context.Cars.Remove(carInDb);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Cars");
        }
        //only admin allowed to update cars
        [Authorize(Roles = "Admin")]
        public ActionResult Update(int id)
        {
            var carInDb = _context.Cars.SingleOrDefault(c => c.CarId == id);

            if (carInDb == null)
            {
                return HttpNotFound();
            }
            return View("CarForm", carInDb);
        }
    }
}
