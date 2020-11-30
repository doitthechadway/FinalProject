using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Auditions.DATA.EF;
using Microsoft.AspNet.Identity;

namespace Auditions.UI.MVC.Controllers
{
    public class ReservationsController : Controller
    {
        private AuditionsEntities db = new AuditionsEntities();

        // GET: Reservations
        [Authorize(Roles = "Admin, Agency, LocationManager")]
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                var reservations = db.Reservations.Include(r => r.Actor).Include(r => r.AuditionLocation).Include(d=>db.UserDetails);

                var reservations2 = db.Reservations.Include(r => r.Actor);
                return View(reservations2.ToList());
            }
            else
            {
                return View("Index", "Home");
            }
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            if (User.IsInRole("Admin"))
            {
                ViewBag.ActorId = new SelectList(db.Actors, "ActorId", "ActorFirstName");
                ViewBag.LocationId = new SelectList(db.AuditionLocations, "LocationID", "LocationName");
                return View();
            }
            else
            {
                string currentUserID = User.Identity.GetUserId();
                ViewBag.ActorId = new SelectList(db.Actors, "ActorId", "ActorFirstName");
                ViewBag.LocationId = new SelectList(db.AuditionLocations, "LocationID", "LocationName");
                return View();
            }

        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AuditionID,ActorId,LocationId,AuditionDate")] Reservation reservation)
        {
            ViewBag.ActorId = new SelectList(db.Actors, "ActorId", "ActorFirstName", reservation.ActorId);
            ViewBag.LocationId = new SelectList(db.AuditionLocations, "LocationID", "LocationName", reservation.LocationId);
            if (ModelState.IsValid)
            {
                var auditionlimit = db.AuditionLocations.Where(x => x.LocationID == reservation.LocationId).Select(x => x.AuditionLimit).FirstOrDefault();

                var nbrOfReservations = db.Reservations.Where(x => x.LocationId == reservation.LocationId).Count();

                if (nbrOfReservations < auditionlimit || User.IsInRole("Admin"))
                {
                    db.Reservations.Add(reservation);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                if (nbrOfReservations > auditionlimit && User.IsInRole("Agency"))
                {
                    ViewBag.Message = $"Sorry, there are no more auditions available at that location.";
                    return View("Create");
                }
            }

            //ViewBag.ActorId = new SelectList(db.Actors, "ActorId", "ActorFirstName", reservation.ActorId);
            //ViewBag.LocationId = new SelectList(db.AuditionLocations, "LocationID", "LocationName", reservation.LocationId);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActorId = new SelectList(db.Actors, "ActorId", "ActorFirstName", reservation.ActorId);
            ViewBag.LocationId = new SelectList(db.AuditionLocations, "LocationID", "LocationName", reservation.LocationId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AuditionID,ActorId,LocationId,AuditionDate")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActorId = new SelectList(db.Actors, "ActorId", "ActorFirstName", reservation.ActorId);
            ViewBag.LocationId = new SelectList(db.AuditionLocations, "LocationID", "LocationName", reservation.LocationId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
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
