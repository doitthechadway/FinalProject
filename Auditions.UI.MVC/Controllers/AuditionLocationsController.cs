using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Auditions.DATA.EF;

namespace Auditions.UI.MVC.Controllers
{
    public class AuditionLocationsController : Controller
    {
        private AuditionsEntities db = new AuditionsEntities();

        // GET: AuditionLocations
        public ActionResult Index()
        {
            return View(db.AuditionLocations.ToList());
        }

        // GET: AuditionLocations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuditionLocation auditionLocation = db.AuditionLocations.Find(id);
            if (auditionLocation == null)
            {
                return HttpNotFound();
            }
            return View(auditionLocation);
        }

        // GET: AuditionLocations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuditionLocations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LocationID,LocationName,Address,City,State,ZipCode,AuditionLimit,AuditionPhoto,AuditionDetails,AuditionDate,IsActive")] AuditionLocation auditionLocation)
        {
            if (ModelState.IsValid)
            {
                db.AuditionLocations.Add(auditionLocation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(auditionLocation);
        }

        // GET: AuditionLocations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuditionLocation auditionLocation = db.AuditionLocations.Find(id);
            if (auditionLocation == null)
            {
                return HttpNotFound();
            }
            return View(auditionLocation);
        }

        // POST: AuditionLocations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LocationID,LocationName,Address,City,State,ZipCode,AuditionLimit,AuditionPhoto,AuditionDetails,AuditionDate,IsActive")] AuditionLocation auditionLocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(auditionLocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(auditionLocation);
        }

        // GET: AuditionLocations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuditionLocation auditionLocation = db.AuditionLocations.Find(id);
            if (auditionLocation == null)
            {
                return HttpNotFound();
            }
            return View(auditionLocation);
        }

        // POST: AuditionLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AuditionLocation auditionLocation = db.AuditionLocations.Find(id);
            db.AuditionLocations.Remove(auditionLocation);
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
