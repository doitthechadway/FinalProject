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
    public class UserDetailsController : Controller
    {
        private AuditionsEntities db = new AuditionsEntities();

        // GET: UserDetails
        public ActionResult Index()
        {
            return View(db.UserDetails.ToList());
        }

        // GET: UserDetails/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail userDetail = db.UserDetails.Find(id);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            return View(userDetail);
        }

        // GET: UserDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,FirstName,LastName,AgencyName,UserPhoto,UserNotes,DateFounded")] UserDetail userDetail)
        {
            if (ModelState.IsValid)
            {             

                db.UserDetails.Add(userDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userDetail);
        }

        // GET: UserDetails/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail userDetail = db.UserDetails.Find(id);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            return View(userDetail);
        }

        // POST: UserDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,FirstName,LastName,AgencyName,UserPhoto,UserNotes,DateFounded")] UserDetail userDetail, HttpPostedFileBase agencyphoto)
        {
            if (ModelState.IsValid)
            {
                if (agencyphoto != null)
                {
                    string imgName = agencyphoto.FileName;
                    string ext = imgName.Substring(imgName.LastIndexOf('.'));
                    string[] goodExts = { ".jpeg", ".jpg", ".gif", ".png" };
                    if (goodExts.Contains(ext.ToLower()) && (agencyphoto.ContentLength <= 4193404))
                    {
                        imgName = Guid.NewGuid() + ext.ToLower();
                        string savePath = Server.MapPath("~/Content/agenylogo/");
                        Image convertedImage = Image.FromStream(agencyphoto.InputStream);
                        int maxImageSize = 500;
                        int maxThumbSize = 100;
                        UploadUtility.ResizeImage(savePath, imgName, convertedImage, maxImageSize, maxThumbSize);

                        UploadUtility.Delete(savePath, userDetail.UserPhoto);

                        userDetail.UserPhoto = imgName;
                    }
                    else
                    {
                        imgName = "nouserimg.png";
                    }
                    userDetail.UserPhoto = imgName;
                }
                db.Entry(userDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userDetail);
        }

        // GET: UserDetails/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail userDetail = db.UserDetails.Find(id);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            return View(userDetail);
        }

        // POST: UserDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UserDetail userDetail = db.UserDetails.Find(id);

            if (userDetail.UserPhoto != null && userDetail.UserPhoto != "nouserimg.png")
            {
                UploadUtility.Delete(Server.MapPath("~/Content/agencylogo/"), userDetail.UserPhoto);
            }
            db.UserDetails.Remove(userDetail);
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
