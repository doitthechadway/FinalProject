using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Auditions.DATA.EF;
using Auditions.UI.MVC.Utilities;

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
        public ActionResult Create([Bind(Include = "LocationID,LocationName,Address,City,State,ZipCode,AuditionLimit,AuditionPhoto,AuditionDetails,AuditionDate,IsActive")] AuditionLocation auditionLocation, HttpPostedFileBase alphoto)
        {
            if (ModelState.IsValid)
            {
                #region Image Upload
                string imgName = "nouserimg.png";

                if (alphoto != null)
                {

                    /**/
                    imgName = alphoto.FileName;

                    /*1*/
                    string ext = imgName.Substring(imgName.LastIndexOf('.'));

                    /*2*/
                    string[] goodExts = { ".jpeg", ".jpg", ".gif", ".png" };

                    /*3*/
                    if (goodExts.Contains(ext.ToLower()) && (alphoto.ContentLength <= 4193404))
                    {
                        /*4*/
                        imgName = Guid.NewGuid() + ext.ToLower();

                        //actorheadshot.SaveAs(Server.MapPath("~/Content/actorheadshots/" + imgName));

                        #region Resize Image
                        /*5*/
                        string savePath = Server.MapPath("~/Content/auditionlocations/");
                        //taking the contents of this file and creating a stream of bytes, http file base type is becoming a stream of bytes into a type of image. this conversion has to take place for us to be able to resize the image
                        /*6*/
                        Image convertedImage = Image.FromStream(alphoto.InputStream);
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
                auditionLocation.AuditionPhoto = imgName;
                #endregion

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
        public ActionResult Edit([Bind(Include = "LocationID,LocationName,Address,City,State,ZipCode,AuditionLimit,AuditionPhoto,AuditionDetails,AuditionDate,IsActive")] AuditionLocation auditionLocation, HttpPostedFileBase alphoto)
        {
            if (ModelState.IsValid)
            {

                #region Image Upload

                if (alphoto != null)
                {
                    /**/
                    string imgName = alphoto.FileName;

                    /*1*/
                    string ext = imgName.Substring(imgName.LastIndexOf('.'));

                    /*2*/
                    string[] goodExts = { ".jpeg", ".jpg", ".gif", ".png" };

                    /*3*/
                    if (goodExts.Contains(ext.ToLower()) && (alphoto.ContentLength <= 4193404))
                    {
                        /*4*/
                        imgName = Guid.NewGuid() + ext.ToLower();

                        //commented this out and followed this other project file
                        //actorheadshot.SaveAs(Server.MapPath("~/Content/auditionlocations/" + imgName));

                        #region Resize Image
                        /*5*/
                        string savePath = Server.MapPath("~/Content/auditionlocations/");
                        //taking the contents of this file and creating a stream of bytes, http file base type is becoming a stream of bytes into a type of image. this conversion has to take place for us to be able to resize the image
                        /*6*/
                        Image convertedImage = Image.FromStream(alphoto.InputStream);
                        int maxImageSize = 500;
                        int maxThumbSize = 100;
                        //if you allowed image uploads for magazine and books - you would need to repeat that code - that's why the image service code is in an imageservice area
                        /*7*/
                        UploadUtility.ResizeImage(savePath, imgName, convertedImage, maxImageSize, maxThumbSize);

                        UploadUtility.Delete(savePath, auditionLocation.AuditionPhoto);

                        auditionLocation.AuditionPhoto = imgName;
                        //saves image onto server - but doesn't update db need to make sure to update what is stored in the db
                        #endregion

                    }
                    else
                    {
                        imgName = "nouserimg.png";

                    }
                    auditionLocation.AuditionPhoto = imgName;
                }

                #endregion

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

            #region Image utility
            if (auditionLocation.AuditionPhoto != null && auditionLocation.AuditionPhoto != "nouserimg.png")
            {
                UploadUtility.Delete(Server.MapPath("~/Content/auditionlocations/"), auditionLocation.AuditionPhoto);
            }

            #endregion

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
