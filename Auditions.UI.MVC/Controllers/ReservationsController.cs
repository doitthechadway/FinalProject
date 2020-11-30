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
            //var auditionlimit = db.AuditionLocations.Where(x => x.LocationID == Reservation.LocationId).Select(x => x.AuditionLimit).FirstOrDefault();

            //var nbrOfReservations = db.Reservations.Where(x => x.LocationId == reservation.LocationId).Count();

            //var nbrOfOpenSpots = auditionlimit - nbrOfReservations;

            var locations = db.AuditionLocations.ToList();

            if (User.IsInRole("Admin"))
            {
                var reservations2 = db.Reservations.Include(r => r.Actor);
                foreach (var r in reservations2)
                {
                    var count = db.Reservations.Where(c => c.LocationId == r.LocationId).Count();
                    r.OpenSpots = r.AuditionLocation.AuditionLimit - count;
                }
                return View(reservations2.ToList());
            }

            else if (User.IsInRole("Agency"))
            {

                string currentUserID = User.Identity.GetUserId();
                var agencyreservations = db.Reservations.Where(r => r.Actor.AgencyID == currentUserID);
                foreach (var r in agencyreservations)
                {
                    var count = db.Reservations.Where(c => c.LocationId == r.LocationId).Count();
                    r.OpenSpots = r.AuditionLocation.AuditionLimit - count;
                }
                return View(agencyreservations.ToList());
            }
            else if (User.IsInRole("LocationManager"))
            {
                string currentUserID = User.Identity.GetUserId();
                var agencyreservations = db.Reservations.Where(r => r.AuditionLocation.LManagerID == currentUserID);
                foreach (var r in agencyreservations)
                {
                    var count = db.Reservations.Where(c => c.LocationId == r.LocationId).Count();
                    r.OpenSpots = r.AuditionLocation.AuditionLimit - count;
                }
                return View(agencyreservations.ToList());

            }
            Session["ErrorMessage"] = "this is my error message";
            return RedirectToAction("Index", "Home");
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
        [Authorize(Roles = "Admin, Agency")]
        public ActionResult Create()
        {
            if (User.IsInRole("Admin"))
            {
                ViewBag.ActorId = new SelectList(db.Actors, "ActorId", "ActorFullName");
                ViewBag.LocationId = new SelectList(db.AuditionLocations, "LocationID", "FullAuditionLocation");
                return View();
            }
            if (User.IsInRole("Agency"))
            {
                string currentUserID = User.Identity.GetUserId();
                ViewBag.ActorId = new SelectList(db.Actors.Where(a => a.AgencyID == currentUserID), "ActorId", "ActorFullName");
                ViewBag.LocationId = new SelectList(db.AuditionLocations, "LocationID", "FullAuditionLocation");

                //ViewBag.AuditionDate = db.AuditionLocations.Where(a => a.AuditionDate == new System.DateTime);

                //ViewBag.AuditionDate = new SelectList(db.AuditionLocations.Where(a => a.AuditionDate.Value.ToString()));
                return View();
            }

            return RedirectToAction("Index", "Home");

        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Agency")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AuditionID,ActorId,LocationId,AuditionDate")] Reservation reservation)
        {
            if (User.IsInRole("Agency"))
            {
                string currentUserID = User.Identity.GetUserId();
                ViewBag.ActorId = new SelectList(db.Actors.Where(a => a.AgencyID == currentUserID), "ActorId", "ActorFullName", reservation.ActorId);
                ViewBag.LocationId = new SelectList(db.AuditionLocations, "LocationID", "FullAuditionLocation", reservation.LocationId);
            }
            if (User.IsInRole("Admin"))
            {
                ViewBag.ActorId = new SelectList(db.Actors, "ActorId", "ActorFullName", reservation.ActorId);
                ViewBag.LocationId = new SelectList(db.AuditionLocations, "LocationID", "FullAuditionLocation", reservation.LocationId);
            }
            if (ModelState.IsValid)
            {
                var auditionlimit = db.AuditionLocations.Where(x => x.LocationID == reservation.LocationId).Select(x => x.AuditionLimit).FirstOrDefault();

                var nbrOfReservations = db.Reservations.Where(x => x.LocationId == reservation.LocationId).Count();

                var nbrOfOpenSpots = auditionlimit - nbrOfReservations;

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
                //assign reservation date
            }

            //ViewBag.ActorId = new SelectList(db.Actors, "ActorId", "ActorFirstName", reservation.ActorId);
            //ViewBag.LocationId = new SelectList(db.AuditionLocations, "LocationID", "LocationName", reservation.LocationId);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        [Authorize(Roles = "Admin, Agency, LocationManager")]
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
            ViewBag.ActorId = new SelectList(db.Actors, "ActorId", "ActorFullName", reservation.ActorId);
            ViewBag.LocationId = new SelectList(db.AuditionLocations, "LocationID", "FullAuditionLocation", reservation.LocationId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Agency, LocationManager")]
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
            ViewBag.ActorId = new SelectList(db.Actors, "ActorId", "ActorFullName", reservation.ActorId);
            ViewBag.LocationId = new SelectList(db.AuditionLocations, "LocationID", "FullAuditionLocation", reservation.LocationId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        [Authorize(Roles = "Admin, Agency, LocationManager")]
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
        [Authorize(Roles = "Admin, Agency, LocationManager")]
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
