using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sozial.Models;

namespace Sozial.Controllers
{
    public class CommentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comment
        public ActionResult Index()
        {
            return View(db.CommentModels.ToList());
        }

        // GET: Comment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentModel commentModel = db.CommentModels.Find(id);
            if (commentModel == null)
            {
                return HttpNotFound();
            }
            return View(commentModel);
        }

        // GET: Comment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "commentID,authorID,postID,commentText")] CommentModel commentModel)
        {
            if (ModelState.IsValid)
            {
                commentModel.authorID = User.Identity.Name;
                commentModel.postID = Int32.Parse(Request.Url.Segments[3]);
                db.CommentModels.Add(commentModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(commentModel);
        }

        // GET: Comment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentModel commentModel = db.CommentModels.Find(id);
            if (commentModel == null)
            {
                return HttpNotFound();
            }
            return View(commentModel);
        }

        // POST: Comment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "commentID,authorID,postID,commentText")] CommentModel commentModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commentModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(commentModel);
        }

        // GET: Comment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentModel commentModel = db.CommentModels.Find(id);
            if (commentModel == null)
            {
                return HttpNotFound();
            }
            return View(commentModel);
        }

        // POST: Comment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CommentModel commentModel = db.CommentModels.Find(id);
            db.CommentModels.Remove(commentModel);
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
