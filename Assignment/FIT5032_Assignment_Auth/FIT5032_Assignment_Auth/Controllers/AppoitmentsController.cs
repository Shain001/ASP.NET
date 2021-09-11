using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_Assignment_Auth.Models;
using Microsoft.AspNet.Identity;

namespace FIT5032_Assignment_Auth.Controllers
{
    public class AppoitmentsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Appoitments
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var appointments = db.Appoitment.Where(s => s.Uid == userId).ToList();
            return View(appointments);
        }

        // GET: Appoitments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appoitment appoitment = db.Appoitment.Find(id);
            if (appoitment == null)
            {
                return HttpNotFound();
            }
            return View(appoitment);
        }

        // GET: Appoitments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Appoitments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,AppDate,Address,Postcode,State,Type,Rate,Uid")] Appoitment appoitment)
        {
            appoitment.Uid = User.Identity.GetUserId();

            ModelState.Clear();
            TryValidateModel(appoitment);

            if (ModelState.IsValid)
            {
                db.Appoitment.Add(appoitment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(appoitment);
        }

        // GET: Appoitments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appoitment appoitment = db.Appoitment.Find(id);
            if (appoitment == null)
            {
                return HttpNotFound();
            }
            return View(appoitment);
        }

        // POST: Appoitments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AppDate,Address,Postcode,State,Type,Rate,Uid")] Appoitment appoitment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appoitment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appoitment);
        }

        // GET: Appoitments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appoitment appoitment = db.Appoitment.Find(id);
            if (appoitment == null)
            {
                return HttpNotFound();
            }
            return View(appoitment);
        }

        // POST: Appoitments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appoitment appoitment = db.Appoitment.Find(id);
            db.Appoitment.Remove(appoitment);
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
