using Assignment.Models;
using Assignment.Utils;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
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
                        es.Send(e, subject, contents, null, null);
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

        //[HttpPost]
        //public ActionResult Admin_send_email(SendEmailViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            List<String> toEmail = model.ToEmail;
        //            String subject = model.Subject;
        //            String contents = model.Contents;

        //            EmailSender es = new EmailSender();

        //            foreach (var e in toEmail)
        //            {
        //                if (e == "-1")
        //                {
        //                    var context = new IdentityDbContext();
        //                    var users = context.Users.ToList();
        //                    foreach (var u in users)
        //                    {
        //                        es.Send(u.Email, subject, contents);
        //                    }
        //                }
        //                else
        //                {
        //                    es.Send(e, subject, contents);
        //                }

        //            }


        //            ViewBag.Result = "Email has been send.";

        //            ModelState.Clear();

        //            return View(new AdminEmail());
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }

        //    return View();
        //}


        [HttpPost]
        public ActionResult Admin_send_email(SendEmailViewModel model, HttpPostedFileBase postedFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // attachment
                    ////var fileName = Path.GetFileName(postedFile.FileName);
                    ////var path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                    ////postedFile.SaveAs(path);
                    
                    string fileName;
                    string file;
                    if (postedFile != null)
                    {
                        MemoryStream target = new MemoryStream();
                        postedFile.InputStream.CopyTo(target); // generates problem in this line
                        byte[] data = target.ToArray();

                        file = Convert.ToBase64String(data);

                        fileName = postedFile.FileName;
                    }
                    else
                    {
                        file = null;
                        fileName = null;
                    }
                    
                    

                    List<String> toEmail = (List<string>)model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;
                    
                    EmailSender es = new EmailSender();

                    foreach (var e in toEmail)
                    {
                        if (e == "-1")
                        {
                            var context = new IdentityDbContext();
                            var users = context.Users.ToList();
                            foreach (var u in users)
                            {
                                es.Send(u.Email, subject, contents,file,fileName);
                            }
                        }
                        else
                        {
                            es.Send(e, subject, contents, file, fileName);
                        }

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