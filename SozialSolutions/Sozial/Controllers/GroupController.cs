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
    public class GroupController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public Repositories.GroupRepo grpRepo = new Repositories.GroupRepo(new ApplicationDbContext());

        // GET: Group
        public ActionResult Index()
        {
            return View(db.GroupModels.ToList());
        }

        // GET: Group/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupModel groupModel = db.GroupModels.Find(id);
            if (groupModel == null)
            {
                return HttpNotFound();
            }
            groupModel.Members = grpRepo.getMembers(groupModel.groupID).ToList();
            groupModel.Posts = grpRepo.getAllPostsForGroup(groupModel.groupID).ToList();

            return View(groupModel);
        }

        


        // GET: Group/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Group/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "groupID,likeCount,groupName,groupDescription,groupPicture,groupBanner")] GroupModel groupModel)
        {
            if (ModelState.IsValid)
            {
                db.GroupModels.Add(groupModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(groupModel);
        }

        // GET: Group/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupModel groupModel = db.GroupModels.Find(id);
            if (groupModel == null)
            {
                return HttpNotFound();
            }
            return View(groupModel);
        }

        // POST: Group/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "groupID,likeCount,groupName,groupDescription,groupPicture,groupBanner")] GroupModel groupModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(groupModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(groupModel);
        }

        // GET: Group/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupModel groupModel = db.GroupModels.Find(id);
            if (groupModel == null)
            {
                return HttpNotFound();
            }
            return View(groupModel);
        }

        // POST: Group/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GroupModel groupModel = db.GroupModels.Find(id);
            db.GroupModels.Remove(groupModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult joinGroup(int id)
        {
            grpRepo.joinGroup(id);
            return RedirectToAction("Index");
        }


    [HttpPost]
        public ActionResult PostToGroup(GroupPostViewModel sunShine)
        {
            return View();
        }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult CreatePost(int? groupID)
        {
            int i = 1;
            groupID = i;
            return View();
        }

       [HttpPost]
       [ValidateAntiForgeryToken]
        public ActionResult CreatePost(int groupID, [Bind(Include = "postID,userID,text,imageUrl")] PostModel groupPostCreate)
        {
            groupID = 1;
            if (ModelState.IsValid)
            {
                 

                db.PostModels.Add(groupPostCreate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(groupPostCreate);
        }

    }
}

