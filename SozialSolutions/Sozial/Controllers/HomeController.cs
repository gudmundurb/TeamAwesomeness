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

        private RelationshipRepo relRepo = new RelationshipRepo();
        private GroupRepo grpRepo = new GroupRepo();

        public ActionResult Index()
        {
            //db connection created
           //ApplicationDbContext db = new ApplicationDbContext();
            //.......not supposed to be in controller


            //Repos created 
            PostRepo pRepo = new PostRepo();
            GameRepo gameRepo = new GameRepo();
            NewsRepo newsRepo = new NewsRepo();
            //End of Repos

            
            FrontPageViewModel fp = new FrontPageViewModel();

            //Get 3 most recent news created
            fp.newestNews = newsRepo.getRecentNews(3).ToList();
            //-------

            //Get 5 most recent groups created
            fp.newestGroups = grpRepo.getRecentGroups(5).ToList();
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
            GameRepo gRepo = new GameRepo();
            fp.hottestGames = gRepo.getHottestGames(3).ToList();
            

            return View(fp);
        }

        public ActionResult About()
        {
            //This makes administrator user for us!!!!
            IdentityManager manager = new IdentityManager();
            if (!manager.RoleExists("Administrators"))
            {
                manager.CreateRole("Administrators");
            }
            if (!manager.UserExists("admin"))
            {
                ApplicationUser newAdmin = new ApplicationUser();
                newAdmin.UserName = "admin";
                manager.CreateUser(newAdmin, "123456");
                manager.AddUserToRole(newAdmin.Id, "Administrators");
            }

            //Shows about us
            //User;
            return View();
        }
        /* >>>>>>>>> postToProfilePLS */
        [Authorize]
        public ActionResult postToProfile(PostToProfileViewModel sunshine)
        {

            sunshine.newPost.userID = User.Identity.Name;
            if (sunshine.newPost.text != null) { 
                relRepo.postToProfile(sunshine.newPost, sunshine.profileOwner);
            }

            return RedirectToAction("Profile", new { username = sunshine.profileOwner});
        }

        [Authorize]
        public ActionResult search()
        {
            return View(relRepo.getAllUsers());
        }

        [Authorize]
        [HttpPost]
        public ActionResult search(string username)
        {
            return View(relRepo.searchFor(username));
        }



        [Authorize]
        public ActionResult UserList()
        {
            IEnumerable<ApplicationUser> users = relRepo.getAllUsers();
            foreach (ApplicationUser user in users)
            {
                user.friends = relRepo.getFriends(user.UserName).ToList();
            }
            return View(users);
        }


        [Authorize]
        public ActionResult add(string name)
        {
            if (name == null) { return RedirectToAction("Search"); }
            relRepo.addFriend(name);

            return RedirectToAction("Profile", new { username = name });
        }


        [Authorize]
        public ActionResult myFriends()
        {
            return View( relRepo.getFriends(User.Identity.Name).ToList() );
        }


        [Authorize]
        public ActionResult unFriend(string exname)
        {
            relRepo.unFriend(exname);
            return RedirectToAction("Profile");
        }


        [Authorize]
        public ActionResult Contact()
        {
            //Shows our news

            return View();
        }


        [Authorize]
        public ActionResult View1()
        {
            ViewBag.Message = "check";
            return View();
        }



        //need authorization !! 
        [Authorize]
        public ActionResult profile(string userName)
        {
            //not allowed to be in controller!!!!! 
            //If username is not passed. or if the user is not real redirect to own profile.
            if (userName == null || !relRepo.realUser(userName))
            {
                userName = User.Identity.Name;
            }

            //if user is not friend, And username is not the users' own name,  Redirect to own profile.
            if (!relRepo.areFriends(userName))
            {
                userName = User.Identity.Name;
            }

            ProfileViewModel profileModel = new ProfileViewModel();

            //this is to get all my friends for profile page

            profileModel.myFriends = relRepo.getFriends(userName).ToList();

            //this is to get all my groups for profile page
            
            profileModel.myGroups = grpRepo.getUserGroup(userName).ToList();
            
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

            profileModel.profileOwner = relRepo.getUser(userName);
            
            profileModel.profilePosts = relRepo.getAllProfilePosts(userName).ToList();

            GameRepo gameRep = new GameRepo(new ApplicationDbContext());

            profileModel.myGames = gameRep.getFaveGamesForUser(userName).ToList();

            return View(profileModel);
        }
    }
}