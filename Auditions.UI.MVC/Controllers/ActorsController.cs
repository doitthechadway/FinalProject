using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Auditions.DATA.EF;
using Auditions.UI.MVC.Controllers;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using Auditions.UI.MVC.Utilities;
using Microsoft.AspNet.Identity;

namespace Auditions.UI.MVC.Controllers
{
    public class ActorsController : Controller
    {
        private AuditionsEntities db = new AuditionsEntities();

        // GET: Actors
        [Authorize(Roles = "Admin, Agency, LocationManager")]
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                var actors = db.Actors.Include(a => a.UserDetail);
                return View(actors.ToList());
            }
            else if (User.IsInRole("Agency"))
            {
                string currentUserID = User.Identity.GetUserId();
                var agencysActors = db.Actors.Where(x => x.AgencyID == currentUserID).Include(a => a.UserDetail);
                return View(agencysActors.ToList());
            }
            else
            {
               //changetosession = "You are not allowed to view the details of this Actor.";
                return RedirectToAction("Index", "Home");
            }

            //var actors = db.Actors.Include(a => a.UserDetail);
            //return View(actors.ToList());
        }

        [Authorize(Roles = "Admin, Agency, LocationManager")]
        public ActionResult ActorTileView()
        {
            if (User.IsInRole("Admin"))
            {
                var actors = db.Actors.Include(a => a.UserDetail);
                return View(actors.ToList());
            }
            else if (User.IsInRole("Agency"))
            {
                string currentUserID = User.Identity.GetUserId();
                var agencysActors = db.Actors.Where(x => x.AgencyID == currentUserID).Include(a => a.UserDetail);
                return View(agencysActors.ToList());
            }
            else
            {
                //changetosession = "You are not allowed to view the details of this Actor.";
                return RedirectToAction("Index", "Home");
            }

            //var actors = db.Actors.Include(a => a.UserDetail);
            //return View(actors.ToList());
        }

        // GET: Actors/Details/5
        [Authorize(Roles = "Admin, Agency")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actor actor = db.Actors.Find(id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
        }
        //else if (User.IsInRole("Agency"))
        //{
        //    string currentUserID = User.Identity.GetUserId();
        //    //var singleActor = db.Actors.Where(x => x.AgencyID == currentUserID).Include;
        //    var singleActor = from a in db.Actors
        //                      where a.AgencyID == currentUserID
        //                      select a;
        //    return View(singleActor.FirstOrDefault());
        //}
        //else
        //{
        //    return View(ViewBag.ErrorMessage = "You are not allowed to view this page.");

        //}


        // GET: Actors/Create
        [Authorize(Roles = "Admin, Agency")]
        public ActionResult Create()
        {
            string currentUserID = User.Identity.GetUserId();
            ViewBag.AgencyID = new SelectList(db.UserDetails.Where(x => x.UserID == currentUserID), "UserID", "FirstName");
            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Agency")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ActorId,ActorFirstName,ActorLastName,Address,City,State,ZipCode,PhoneNumber,AgencyID,ActorPhoto,SpecialNotes,DateAdded,IsActive")] Actor actor, HttpPostedFileBase actorheadshot)
        {

            ViewBag.AgencyID = new SelectList(db.UserDetails, "UserID", "FirstName", actor.AgencyID);
            if (ModelState.IsValid)
            {
                #region Image Upload
                string imgName = "~/Content/actorheadshots/nouserimg.png";

                if (actorheadshot != null)
                {

                    /**/
                    imgName = actorheadshot.FileName;

                    /*1*/
                    string ext = imgName.Substring(imgName.LastIndexOf('.'));

                    /*2*/
                    string[] goodExts = { ".jpeg", ".jpg", ".gif", ".png" };

                    /*3*/
                    if (goodExts.Contains(ext.ToLower()) && (actorheadshot.ContentLength <= 4193404))
                    {
                        /*4*/
                        imgName = Guid.NewGuid() + ext.ToLower();

                        //actorheadshot.SaveAs(Server.MapPath("~/Content/actorheadshots/" + imgName));

                        #region Resize Image
                        /*5*/
                        string savePath = Server.MapPath("~/Content/actorheadshots/");
                        //taking the contents of this file and creating a stream of bytes, http file base type is becoming a stream of bytes into a type of image. this conversion has to take place for us to be able to resize the image
                        /*6*/
                        Image convertedImage = Image.FromStream(actorheadshot.InputStream);
                        int maxImageSize = 500;
                        int maxThumbSize = 100;
                        //if you allowed image uploads for magazine and books - you would need to repeat that code - that's why the image service code is in an imageservice area
                        /*7*/
                        UploadUtility.ResizeImage(savePath, imgName, convertedImage, maxImageSize, maxThumbSize);
                        //saves image onto server - but doesn't update db need to make sure to update what is stored in the db
                        #endregion

                    }
                    else
                    {
                        imgName = "nouserimg.png";
                    }
                }
                actor.ActorPhoto = imgName;
                #endregion

                db.Actors.Add(actor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(actor);
        }

        // GET: Actors/Edit/5
        [Authorize(Roles = "Admin, Agency")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actor actor = db.Actors.Find(id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgencyID = new SelectList(db.UserDetails, "UserID", "FirstName", actor.AgencyID);
            return View(actor);

            //if (User.IsInRole("Agency"))
            //{
            //    string currentUserID = User.Identity.GetUserId();
            //    ViewBag.AgencyID = new SelectList(db.UserDetails.Where(x => x.UserID == currentUserID), "UserID", "FirstName", actor.AgencyID);
            //    return View(actor);
            //}

        }

        // POST: Actors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Agency")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ActorId,ActorFirstName,ActorLastName,Address,City,State,ZipCode,PhoneNumber,AgencyID,ActorPhoto,SpecialNotes,DateAdded,IsActive")] Actor actor, HttpPostedFileBase actorheadshot)
        {
            if (ModelState.IsValid)
            {
                #region Image Upload

                if (actorheadshot != null)
                {
                    /**/
                    string imgName = actorheadshot.FileName;

                    /*1*/
                    string ext = imgName.Substring(imgName.LastIndexOf('.'));

                    /*2*/
                    string[] goodExts = { ".jpeg", ".jpg", ".gif", ".png" };

                    /*3*/
                    if (goodExts.Contains(ext.ToLower()) && (actorheadshot.ContentLength <= 4193404))
                    {
                        /*4*/
                        imgName = Guid.NewGuid() + ext.ToLower();

                        //commented this out and followed this other project file
                        //actorheadshot.SaveAs(Server.MapPath("~/Content/actorheadshots/" + imgName));

                        #region Resize Image
                        /*5*/
                        string savePath = Server.MapPath("~/Content/actorheadshots/");
                        //taking the contents of this file and creating a stream of bytes, http file base type is becoming a stream of bytes into a type of image. this conversion has to take place for us to be able to resize the image
                        /*6*/
                        Image convertedImage = Image.FromStream(actorheadshot.InputStream);
                        int maxImageSize = 500;
                        int maxThumbSize = 100;
                        //if you allowed image uploads for magazine and books - you would need to repeat that code - that's why the image service code is in an imageservice area
                        /*7*/
                        UploadUtility.ResizeImage(savePath, imgName, convertedImage, maxImageSize, maxThumbSize);

                        UploadUtility.Delete(savePath, actor.ActorPhoto);

                        actor.ActorPhoto = imgName;
                        //saves image onto server - but doesn't update db need to make sure to update what is stored in the db
                        #endregion

                    }
                    else
                    {
                        imgName = "nouserimg.png";

                    }
                    actor.ActorPhoto = imgName;
                }

                #endregion

                db.Entry(actor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AgencyID = new SelectList(db.UserDetails, "UserID", "FirstName", actor.AgencyID);
            return View(actor);
        }

        // GET: Actors/Delete/5
        [Authorize(Roles = "Admin, Agency")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actor actor = db.Actors.Find(id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
        }

        // POST: Actors/Delete/5
        [Authorize(Roles = "Admin, Agency")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Actor actor = db.Actors.Find(id);
            #region Image utility
            if (actor.ActorPhoto != null && actor.ActorPhoto != "nouserimg.png")
            {
                UploadUtility.Delete(Server.MapPath("~/Content/actorheadshots/"), actor.ActorPhoto);
            }
            #endregion

            db.Actors.Remove(actor);
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
