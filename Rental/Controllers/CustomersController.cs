using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rental.Models;
using Microsoft.AspNet.Identity;

namespace Rental.Controllers
{
    [System.Web.Mvc.Authorize(Roles = "Admin")]
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;


        public CustomersController()
        {
            _context = new ApplicationDbContext();    
        }

        // GET: Customers
        public ActionResult Index()
        {
            var customers = _context.Customers.ToList();
            return View(customers);
        }
        public ActionResult New()
        {
            var customer = new Customer();
            return View("CustomerForm", customer);
        }

        public ActionResult Save(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if(customer.CustomerId == 0)
                {
                    customer.UserId = User.Identity.GetUserId();
                    _context.Customers.Add(customer);
                }
                else
                {
                    var customerInDb = _context.Customers.FirstOrDefault(c => c.CustomerId == customer.CustomerId);
                    customerInDb.FirstName = customer.FirstName;
                    customerInDb.LastName = customer.LastName;
                    customerInDb.Email = customer.Email;
                    customerInDb.Mobile = customer.Mobile;
                 
                }
                _context.SaveChanges();        
            }
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Delete(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.CustomerId == id);

            if (customerInDb != null)
            {
                _context.Customers.Remove(customerInDb);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Customers");
        }


        public ActionResult Update(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.CustomerId == id);

            if(customer == null)
            {
                return HttpNotFound();
            }
            return View("CustomerForm", customer);
        }
    }
}