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
            var average = db.Appointment.Average(s => s.Rate);
            ViewBag.Message = average;
            return View();
        }
    }
}