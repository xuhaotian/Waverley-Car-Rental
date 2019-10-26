using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rental.ViewModels;
using Rental.Utils;
using System.IO;
using Rental.Models;

namespace Rental.Controllers
{
    
    public class HomeController : Controller
    {
        //private ApplicationDbContext _context;
        public ActionResult Index()
        {
            return View();
        }

        //send email via contact page
        public ActionResult Contact()
        {
            return View(new SendEmailViewModel());
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Contact(SendEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    String toEmail = model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;

                    var attachment = Request.Files["attachment"];

                    //check upload
                    if (attachment.ContentLength > 0)
                    {
                        String path = Path.Combine(Server.MapPath("~/Content/Attachment/"), attachment.FileName);
                        attachment.SaveAs(path);

                        EmailSender es = new EmailSender();
                        es.SendAttachment(toEmail, subject, contents, path, attachment.FileName);
                    }
                    else
                    {
                        EmailSender es = new EmailSender();
                        es.Send(toEmail, subject, contents);
                    }
                    // save file to server


                    ViewBag.Result = "Email has been send.";

                    ModelState.Clear();

                    return View(new SendEmailViewModel());
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }





       




        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }



        // bulk email
        //public ActionResult About()
        //{
        //    return View(new SendEmailViewModel());
        //}
        //[HttpPost]
        //[ValidateInput(false)]
        //public ActionResult About(SendEmailViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            String toEmail = model.ToEmail;

        //            string bulk = Request["bulk"];
        //            if (bulk == null)
        //            {
        //                toEmail = model.ToEmail;
        //            }
        //            else
        //            {
        //                string query = "select Email from AspNetUsers";
        //                var emails = _context.Database.SqlQuery<string>(query).ToArray();
        //                int count = 0;
        //                foreach (string email in emails)
        //                {
        //                    count++;

        //                    toEmail += email;
        //                    if (count < emails.Length)
        //                    {
        //                        toEmail += ",";
        //                    }
        //                }
        //            }

        //            String subject = model.Subject;
        //            String contents = model.Contents;

        //            var attachment = Request.Files["attachment"];

        //            //check upload
        //            if (attachment.ContentLength > 0)
        //            {
        //                String path = Path.Combine(Server.MapPath("~/Content/Attachment/"), attachment.FileName);
        //                attachment.SaveAs(path);

        //                EmailSender es = new EmailSender();
        //                es.SendAttachment(toEmail, subject, contents, path, attachment.FileName);
        //            }
        //            else
        //            {
        //                EmailSender es = new EmailSender();
        //                es.Send(toEmail, subject, contents);
        //            }
        //            // save file to server


        //            ViewBag.Result = "Email has been send.";

        //            ModelState.Clear();

        //            return View(new SendEmailViewModel());
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }

        //    return View();
        //}






        // end
    }
}