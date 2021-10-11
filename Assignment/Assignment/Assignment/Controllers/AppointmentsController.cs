using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assignment.Models;
using Microsoft.AspNet.Identity;

namespace Assignment.Controllers
{
    public class AppointmentsController : Controller
    {
        private Model2 db = new Model2();

        // GET: Appointments
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            //var appointments = db.Appoitment.Where(s => s.Uid == userId).ToList();
            var appointment = db.Appointment.Where(s => s.UID == userId).Include(a => a.ServiceType);
            return View(appointment.ToList());
        }

        // GET: Appointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointment.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Appointments/Create
        public ActionResult Create()
        {
            ViewBag.TypeId = new SelectList(db.ServiceType, "TypeId", "TypeName");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "AppId,AppDate,AppAddress,UID,TypeId,Rate")] Appointment appointment)
        {
            appointment.UID = User.Identity.GetUserId();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(HttpUtility.HtmlEncode(appointment.AppAddress));

            sb.Replace("<b>", "");
            sb.Replace("</b>", "");
            sb.Replace("<script>", "");
            sb.Replace("</script>", "");

            appointment.AppAddress = sb.ToString();

            string ad = HttpUtility.HtmlEncode(appointment.AppAddress);
            appointment.AppAddress = ad;

            ModelState.Clear();
            TryValidateModel(appointment);

            // validation part

            var app = db.Appointment.Where(s => s.AppDate == appointment.AppDate).Where(s=>s.AppAddress == appointment.AppAddress).FirstOrDefault();

            if (app == null)
            {
                if (ModelState.IsValid)
                {
                    db.Appointment.Add(appointment);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.TypeId = new SelectList(db.ServiceType, "TypeId", "TypeName", appointment.TypeId);
                return View(appointment);

            }
            else
            {
                ModelState.AddModelError(nameof(Appointment.AppAddress), "You have already make an Appointment for same address and time");
                ViewBag.TypeId = new SelectList(db.ServiceType, "TypeId", "TypeName", appointment.TypeId);
                return View(appointment);
            }

            

            
        }

        // GET: Appointments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointment.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.TypeId = new SelectList(db.ServiceType, "TypeId", "TypeName", appointment.TypeId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "AppId,AppDate,AppAddress,UID,TypeId,Rate")] Appointment appointment)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(HttpUtility.HtmlEncode(appointment.AppAddress));

            sb.Replace("<b>", "");
            sb.Replace("</b>", "");
            sb.Replace("<script>", "");
            sb.Replace("</script>", "");

            appointment.AppAddress = sb.ToString();

            string ad = HttpUtility.HtmlEncode(appointment.AppAddress);
            appointment.AppAddress = ad;

            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TypeId = new SelectList(db.ServiceType, "TypeId", "TypeName", appointment.TypeId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointment.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointment.Find(id);
            db.Appointment.Remove(appointment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult Rate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointment.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.TypeId = new SelectList(db.ServiceType, "TypeId", "TypeName", appointment.TypeId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Rate([Bind(Include = "AppId,Rate")] Appointment appointment)
        {
            if (appointment.AppId != null)
            {
                var app = db.Appointment.Where(s => s.AppId == appointment.AppId).FirstOrDefault();
                app.Rate = appointment.Rate;
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TypeId = new SelectList(db.ServiceType, "TypeId", "TypeName", appointment.TypeId);
            return View(appointment);
        }
    }
}
