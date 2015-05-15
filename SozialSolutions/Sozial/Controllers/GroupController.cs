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
    [Authorize]
    public class GroupController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        public Repositories.GroupRepo grpRepo = new Repositories.GroupRepo(new ApplicationDbContext());

        // GET: Group
        public ActionResult Index()
        {
            return View(grpRepo.getAllGroups());
        }

        // GET: Group/Details/5
        public ActionResult Details(int id)
        {
            

            GroupModel groupModel = grpRepo.getGroupById(id);
            if (groupModel == null)
            {
                return HttpNotFound();
            }
            groupModel.Members = grpRepo.getMembers(groupModel.groupID).ToList();
            groupModel.Posts = grpRepo.getAllPostsForGroup(groupModel.groupID).ToList();

            if (!grpRepo.inGroup(groupModel.groupID))
            {
                return RedirectToAction("../Group/Index");
            }

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
                grpRepo.createGroup(groupModel);
                return RedirectToAction("Index");
            }

            return View(groupModel);
        }

        // GET: Group/Edit/5
        public ActionResult Edit(int id)
        {
            
            GroupModel groupModel = grpRepo.getGroupById(id);
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
                grpRepo.editGroup(groupModel);
                return RedirectToAction("Index");
            }
            return View(groupModel);
        }

        // GET: Group/Delete/5
        public ActionResult Delete(int id)
        {
            
            GroupModel groupModel = grpRepo.getGroupById(id);
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
            GroupModel groupModel = grpRepo.getGroupById(id);
            grpRepo.deleteGroup(groupModel);
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
            sunShine.newPost.userID = User.Identity.Name;

            if (sunShine.newPost.text != null) { 
                grpRepo.addPostToGroup(sunShine.newPost, sunShine.groupId);
            }

            return RedirectToAction("Details", new {id = sunShine.groupId });
        }
    }
}

