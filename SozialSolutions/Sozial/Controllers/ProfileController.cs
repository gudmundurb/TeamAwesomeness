using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sozial.Models;
using SozialProject.Models;

namespace Sozial.Controllers
{
    public class ProfileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Profile
        public ActionResult Index()
        {
            return View(db.ProfileModels.ToList());
        }

        // GET: Profile/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileModel profileModel = db.ProfileModels.Find(id);
            if (profileModel == null)
            {
                return HttpNotFound();
            }
            return View(profileModel);
        }

        // GET: Profile/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profile/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userID,userName,userProfilePic,userBannerPic")] ProfileModel profileModel)
        {
            if (ModelState.IsValid)
            {
                db.ProfileModels.Add(profileModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(profileModel);
        }

        // GET: Profile/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileModel profileModel = db.ProfileModels.Find(id);
            if (profileModel == null)
            {
                return HttpNotFound();
            }
            return View(profileModel);
        }

        // POST: Profile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userID,userName,userProfilePic,userBannerPic")] ProfileModel profileModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profileModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profileModel);
        }

        // GET: Profile/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileModel profileModel = db.ProfileModels.Find(id);
            if (profileModel == null)
            {
                return HttpNotFound();
            }
            return View(profileModel);
        }

        // POST: Profile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProfileModel profileModel = db.ProfileModels.Find(id);
            db.ProfileModels.Remove(profileModel);
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
