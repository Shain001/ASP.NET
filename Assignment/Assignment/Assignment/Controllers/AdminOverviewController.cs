using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Assignment.Models;

namespace Assignment.Controllers
{
    public class AdminOverviewController : Controller
    {
        private Model2 db = new Model2();
        // GET: AdminOverview
        public ActionResult Index()
        {
            // For Rating Aggregationg
            // var average = db.Appointment.Average(s => s.Rate);
            // ViewBag.Message = average;

            var avg = db.Appointment.GroupBy(i => i.TypeId)
                .Select(g => new Group<string,string>
                {
                    //Key = 
                    //g.Key.ToString(),
                    Key = db.ServiceType.Where(s => s.TypeId == g.Key).Select(s => s.TypeName).FirstOrDefault(),
            Values = g.Average(i => i.Rate).ToString()
                });

            //foreach (var temp in avg)
            //{
            //    temp.Key = db.ServiceType.Where(s => s.TypeId.ToString() == temp.Key).Select(s => s.TypeName).FirstOrDefault();
            //}

            
            
            return View(avg.ToList());
        }

        public JsonResult GetEvents()
        {
            var events = db.Events.ToList();
            
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult SaveEvent(Events e)
        {
            var status = false;

            if (e.EventID > 0)
            {
                var v = db.Events.Where(a => a.EventID == e.EventID).FirstOrDefault();
                if (v != null)
                {
                    v.Subject = e.Subject;
                    v.Start = e.Start;
                    v.End = e.End;
                    v.Description = e.Description;
                    v.IsFullDay = e.IsFullDay;
                    v.ThemeColor = e.ThemeColor;
                }
            }
            else
            {
                db.Events.Add(e);
            }
            db.SaveChanges();
            status = true;
            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult DeleteEvent(int eventID)
        {
            var status = false;
            var v = db.Events.Where(a => a.EventID == eventID).FirstOrDefault();
            if (v != null)
            {
                db.Events.Remove(v);
                db.SaveChanges();
                status = true;
            }

            return new JsonResult { Data = new { status = status } };
        }
    }
}