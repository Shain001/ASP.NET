using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assignment.Models;
using Assignment.Utils;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Assignment.Controllers
{
    public class AppointmentsAdminController : Controller
    {
        private Model2 db = new Model2();
        private List<SelectListItem> useremails; 
        // GET: AppointmentsAdmin
        public ActionResult Index()
        {
            var appointment = db.Appointment.Include(a => a.ServiceType).ToList();

            var context = new IdentityDbContext();
            
            //return View(appointment);

            List<AppointmentAdmin> apps = new List<AppointmentAdmin>();
            foreach (var a in appointment)
            {
                var user = context.Users.Where(s => s.Id == a.UID).FirstOrDefault();
                var email = user.Email;
   
                a.UID = email;
            }
            
            return View(appointment);

        }

        // GET: AppointmentsAdmin/Details/5
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

        // GET: AppointmentsAdmin/Create
        public ActionResult Create()
        {
            ViewBag.UEmails = useremails;
            ViewBag.TypeId = new SelectList(db.ServiceType, "TypeId", "TypeName");
            return View();
        }

        // POST: AppointmentsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppId,AppDate,AppAddress,UID,TypeId,Rate")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                if (appointment.AppDate.ToString() == "0001/1/1 0:00:00")
                {
                    ModelState.AddModelError(nameof(Appointment.AppDate), "Date Cannot be Null");
                    ViewBag.TypeId = new SelectList(db.ServiceType, "TypeId", "TypeName", appointment.TypeId);
                    return View(appointment);
                }

                //if (appointment.AppAddress == null)
                //{
                //    ModelState.AddModelError(nameof(Appointment.AppAddress), "Address Cannot be Null");
                //    ViewBag.TypeId = new SelectList(db.ServiceType, "TypeId", "TypeName", appointment.TypeId);
                //    return View(appointment);
                //}

                var context = new IdentityDbContext();
                
                 var user = context.Users.Where(s => s.Email == appointment.UID).FirstOrDefault();
                 var uid = user.Id;
                 appointment.UID = uid;
                
                
                
                

                db.Appointment.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TypeId = new SelectList(db.ServiceType, "TypeId", "TypeName", appointment.TypeId);
            return View(appointment);
        }

        // GET: AppointmentsAdmin/Edit/5
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

        // POST: AppointmentsAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppId,AppDate,AppAddress,TypeId")] Appointment appointment)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(appointment).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            if (appointment.AppDate.ToString() == "0001/1/1 0:00:00")
            {
                ModelState.AddModelError(nameof(Appointment.AppDate), "Date Cannot be Null");
                ViewBag.TypeId = new SelectList(db.ServiceType, "TypeId", "TypeName", appointment.TypeId);
                return View(appointment);
            }

            if (appointment.AppAddress == null)
            {
                ModelState.AddModelError(nameof(Appointment.AppAddress), "Address Cannot be Null");
                ViewBag.TypeId = new SelectList(db.ServiceType, "TypeId", "TypeName", appointment.TypeId);
                return View(appointment);
            }

            var app = db.Appointment.Where(s => s.AppId == appointment.AppId).FirstOrDefault();
            app.AppDate = appointment.AppDate;
            app.AppAddress = appointment.AppAddress;
            app.TypeId = appointment.TypeId;
            
            db.SaveChanges();
            
            
            return RedirectToAction("Index");


            //ViewBag.TypeId = new SelectList(db.ServiceType, "TypeId", "TypeName", appointment.TypeId);
            //return View(appointment);
        }

        // GET: AppointmentsAdmin/Delete/5
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

        // POST: AppointmentsAdmin/Delete/5
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
    }
}
