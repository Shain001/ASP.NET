using Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Assignment.Controllers
{
    public class AppCalendarController : Controller
    {
        private Model2 db = new Model2();
        // GET: AppCalendar
        public ActionResult Index()
        {
            var types = db.ServiceType.ToList();
            //List<SelectListItem> selects = new List<SelectListItem>();
            //foreach (var t in types)
            //{
            //    selects.Add(new SelectListItem() { Value=t.TypeId.ToString(), Text=t.TypeName});

            //}

            ViewBag.Selections = types;
            return View();
        }

        public ActionResult AdminIndex()
        {
            var types = db.ServiceType.ToList();
            var context = new IdentityDbContext();
            var users = context.Users.ToList();
            ViewBag.Us = users;
            //List<SelectListItem> selects = new List<SelectListItem>();
            //foreach (var t in types)
            //{
            //    selects.Add(new SelectListItem() { Value=t.TypeId.ToString(), Text=t.TypeName});

            //}

            ViewBag.Selections = types;
            return View();
        }

        [HttpGet]
        public JsonResult GetEvents()
        {

            var userId = User.Identity.GetUserId();
            var events = db.Appointment.Where(a => a.UID == userId).Include(a => a.ServiceType).ToList();
            var context = new IdentityDbContext();
            if (User.IsInRole("Admin"))
            {
                events = db.Appointment.Include(a => a.ServiceType).ToList();
                foreach (var e in events)
                {
                    var id = e.UID;
                    var user = context.Users.Where(s => s.Id == id).FirstOrDefault();
                    e.UID = user.Email;
                }
            }

            //var events = db.Appointment.ToList();

            //Console.WriteLine(events.Count());



            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult SaveEvent(Appointment e)
        {
            var status = false;
            var ser = db.ServiceType.Where(s => s.TypeId == e.TypeId).FirstOrDefault();
            if (e.AppId > 0)
            {
                var v = db.Appointment.Where(a => a.AppId == e.AppId).FirstOrDefault();
                
                if (v != null)
                {
                    v.AppDate = e.AppDate;
                    v.AppAddress = e.AppAddress;
                    v.TypeId = e.TypeId;
                    v.ServiceType = ser;
        
                }
            }
            else
            {
                var existApp = db.Appointment.Where(a => DbFunctions.TruncateTime(a.AppDate) == e.AppDate.Date && a.AppAddress == e.AppAddress).FirstOrDefault();
                if (existApp != null)
                {
                    return new JsonResult { Data = new { status = status } };
                }
                
                if (User.IsInRole("Admin"))
                {
                    var userId = e.UID;
                    e.UID = userId;
                    e.ServiceType = ser;
                    db.Appointment.Add(e);
                }
                else
                {
                    var userId = User.Identity.GetUserId();
                    e.UID = userId;
                    e.ServiceType = ser;
                    db.Appointment.Add(e);
                }
                
            }
            db.SaveChanges();
            status = true;
            return new JsonResult { Data = new { status = status } };
        }

    }
}