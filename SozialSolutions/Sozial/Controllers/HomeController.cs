using System;
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
            //.......not supposed to be in controller


            //Repos created 
            GroupRepo groupRepo = new GroupRepo(db);
            RelationshipRepo relRepo = new RelationshipRepo(db);
            PostRepo pRepo = new PostRepo(db);
            GameRepo gameRepo = new GameRepo(db);
            NewsRepo newsRepo = new NewsRepo(db);
            //End of Repos

            
            FrontPageViewModel fp = new FrontPageViewModel();

            //Get 3 most recent news created
            fp.newestNews = newsRepo.getRecentNews(3).ToList();
            //-------

            //Get 5 most recent groups created
            fp.newestGroups = groupRepo.getRecentGroups(5).ToList();
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
            fp.newestPosts = Posts.OrderByDescending(Poste => Poste.createdDate).Take(5).ToList();
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

      /*  public ActionResult profile()
        {
            return View(profile(User.Identity.Name));
        }

        */
        public ActionResult profile(string userId)
        {
            string userName = userId;
            if (userName == null)
            {
                userName = User.Identity.Name;
            }

           //not allowed to be in controller!!!!! 
            ApplicationDbContext db = new ApplicationDbContext();

            ProfileViewModel profileModel = new ProfileViewModel();

            //this is to get all my friends for profile page
            RelationshipRepo relRepo = new RelationshipRepo(db);
            profileModel.myFriends = relRepo.getFriends(userName).ToList();

            //this is to get all my groups for profile page
            GroupRepo gRepo = new GroupRepo(db);
            profileModel.myGroups = gRepo.getUserGroup(userName).ToList();
            
            //this is to get all my posts AND my friends posts then I will take 10 new posts
            List<PostModel> posts = new List<PostModel>();
            foreach (ApplicationUser friend in profileModel.myFriends)
            {
                IEnumerable<PostModel> temp = relRepo.getAllPostsForUser(friend.UserName);
                foreach (PostModel model in temp)
                {
                    posts.Add(model);
                }

            }
            IEnumerable<PostModel> tempo = relRepo.getAllPostsForUser(userName);
            foreach (PostModel model in tempo)
            {
                posts.Add(model);
            }
            profileModel.newestPosts = posts.OrderByDescending(x => x.createdDate).Take(10).ToList();





            return View(profileModel);
        }
    }
}