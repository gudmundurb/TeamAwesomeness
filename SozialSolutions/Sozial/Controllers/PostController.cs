using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sozial.Models;
using Sozial.Repositories;

namespace Sozial.Controllers
{
    public class PostController : Controller
    {
        private IPostRepo postRepo =  null; //new IGameRepo();
        
        public PostController()
        {
            this.postRepo = new PostRepo(new ApplicationDbContext());
        }

        //private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Post
        public ActionResult Index()
        {
            return View(postRepo.GetPost());
        }

        // GET: Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostModel postModel = postRepo.GetPostByID(id);
            if (postModel == null)
            {
                return HttpNotFound();
            }
            return View(postModel);
        }

        // GET: Post/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Post/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "postID,userID,text,imageUrl")] PostModel postModel)
        {
            if (ModelState.IsValid)
            {
                postModel.userID = User.Identity.Name;
                postRepo.InsertPost(postModel);
                postRepo.SavePost();
                return RedirectToAction("Index");
            }
            return View(postModel);
        }

        // GET: Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostModel postModel = postRepo.GetPostByID(id);
            if (postModel == null)
            {
                return HttpNotFound();
            }
            return View(postModel);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "postID,userID,text,imageUrl")] PostModel postModel)
        {
            if (ModelState.IsValid)
            {
                postRepo.UpdatePost(postModel);

                postRepo.SavePost();
                return RedirectToAction("Index");
            }
            return View(postModel);
        }

        // GET: Post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostModel postModel = postRepo.GetPostByID(id);
            if (postModel == null)
            {
                return HttpNotFound();
            }
            return View(postModel);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PostModel postModel = postRepo.GetPostByID(id);
            postRepo.DeletePost(id);
            postRepo.SavePost();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                postRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
