using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sozial.Models;
namespace Sozial.Controllers

{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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