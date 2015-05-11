﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sozial.Models;
using Sozial.Repositories;
namespace Sozial.Controllers

{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //db connection created
           ApplicationDbContext db = new ApplicationDbContext();
            //.......


            //Repos created 
            GroupRepo gRepo = new GroupRepo(db);
            RelationshipRepo relRepo = new RelationshipRepo(db);
            PostRepo pRepo = new PostRepo(db);
            GameRepo gameRepo = new GameRepo(db);
            //End of Repos

            
            FrontPageViewModel fp = new FrontPageViewModel();


            //Get most recent groups created
            fp.newestGroups = gRepo.getRecentGroups(5).ToList();
            //end of get most recent groups created


            //Get 5 new posts from friends
            IEnumerable<ApplicationUser> Friends = relRepo.getFriends(User.Identity.Name);
            List<PostModel> Posts = new List<PostModel>();
            foreach (ApplicationUser Friend in Friends)
            {
                IEnumerable<PostModel> temp = pRepo.GetByUserID(Friend.UserName);
                foreach (PostModel post in temp)
                {
                    Posts.Add(post);
                }

            }
            fp.newestPosts = Posts.OrderBy(Poste => Poste.createdDate).Take(5).ToList();
            //End of Get 5 new posts from friends


            //fp.newGroupPosts = db.GroupModels ..

            //fp.newestGames = db.GameModels .. 
            return View(fp);
        }

        public ActionResult About()
        {
            //Shows about us
            //User;
            return View();
        }

        public ActionResult UserList()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Repositories.RelationshipRepo relRepo = new Repositories.RelationshipRepo(db);
            IEnumerable<ApplicationUser> users = db.Users.ToList();
            foreach (ApplicationUser user in users)
            {
                user.friends = relRepo.getFriends(user.UserName).ToList();
            }
            return View(users);
        }

        public ActionResult add(string name)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Repositories.RelationshipRepo relRepo = new Repositories.RelationshipRepo(db);
            if (name == null) { return RedirectToAction("UserList"); }
            relRepo.addFriend(name);

            return RedirectToAction("UserList");
        }

        public ActionResult myFriends()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Repositories.RelationshipRepo james_doohan = new Repositories.RelationshipRepo(db);
            return View(james_doohan.getFriends(User.Identity.Name).ToList() );
        }

        public ActionResult unFriend(string exname)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Repositories.RelationshipRepo relRepo = new Repositories.RelationshipRepo(db);
            relRepo.unFriend(exname);
            return RedirectToAction("myFriends");
        }

        public ActionResult Contact()
        {
            //Shows our news

            return View();
        }

        public ActionResult View1()
        {
            ViewBag.Message = "check";
            return View();
        }
    }
}