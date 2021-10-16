using Assignment.Models;
using Assignment.Utils;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment.Controllers
{
    public class HomeController : Controller
    {
        

        //public HomeController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        //public HomeController()
        //{
            
        //}


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Send_Email()
        {

            return View(new SendEmailViewModel());
        }

        [HttpPost]
        public ActionResult Send_Email(SendEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    List<String> toEmail = model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;

                    EmailSender es = new EmailSender();

                    foreach(var e in toEmail)
                    {
                        es.Send(e, subject, contents);
                    }
                    

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


        public ActionResult Admin_send_email()
        {
            var context = new IdentityDbContext();
            var users = context.Users.ToList();
            List<SelectListItem> emails = new List<SelectListItem>();
            foreach (var u in users)
            {
                emails.Add(new SelectListItem { Value = u.Email, Text = u.Email});
            }
            ViewBag.Emails = emails;
            SubjectList.Emails = emails;
            return View(new AdminEmail());
        }

        [HttpPost]
        public ActionResult Admin_send_email(SendEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    List<String> toEmail = model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;

                    EmailSender es = new EmailSender();

                    foreach (var e in toEmail)
                    {
                        es.Send(e, subject, contents);
                    }


                    ViewBag.Result = "Email has been send.";

                    ModelState.Clear();

                    return View(new AdminEmail());
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }
    }

}