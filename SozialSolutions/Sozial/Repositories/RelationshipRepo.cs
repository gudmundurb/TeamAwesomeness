using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sozial.Models;
using System.Data.Entity;

namespace Sozial.Repositories
{
    public class RelationshipRepo : IRelationshipRepo, IDisposable
    {

        public ApplicationDbContext db = null;

        public RelationshipRepo(ApplicationDbContext database)
        {
            db = database;
        }

        public RelationshipRepo()
        {
            db = new ApplicationDbContext();
        }

        public string getSteamId()
        {
            return (from ApplicationUser n in db.Users
                    where n.UserName == System.Web.HttpContext.Current.User.Identity.Name
                    select n.steamId).SingleOrDefault();
        }

        public bool updateUser(ApplicationUser user)
        {
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }


        public bool realUser(string username)
        {
            ApplicationUser user = (from ApplicationUser users in db.Users
                                   where users.UserName == username
                                   select users).SingleOrDefault();
            return(user != null);
        }

        public IEnumerable<ApplicationUser> getAllUsers()
        {
            return db.Users.ToList();
        }

        public IEnumerable<ApplicationUser> searchFor(string username)
        {
            return (from ApplicationUser user in db.Users
                    where user.UserName.Contains(username)
                    select user).ToList();
        }




        /* FRIENDS RELATIONSHIPS */


        public IEnumerable<PostModel> getAllProfilePosts(string profileOwner)
        {

            IEnumerable<int> postIds = (from ProfilePostRelationModel rel in db.ProfilePostRelationModels
                                        where rel.UserId == profileOwner
                                        select rel.postId).ToList(); 

            List<PostModel> posts = new List<PostModel>();
            PostRepo pRepo = new PostRepo(db);

            foreach (int i in postIds)
            {
                posts.Add(pRepo.GetPostByID(i));
            }
            return posts.ToList();
        }



        public IEnumerable<ApplicationUser> getFriends(string username)
        {
            //find all instances of relationship where username is present
            IEnumerable<FriendshipModel> friends = (from FriendshipModel rel in db.FriendshipModels
                                                    where rel.UsernameOne == username || rel.UsernameTwo == username
                                                    select rel).ToList();

            //create a list of usernames. excluding username.
            List<string> usernmList = new List<string>();
            foreach (FriendshipModel relationship in friends)
            {
                if (relationship.UsernameOne == username) { usernmList.Add(relationship.UsernameTwo);  }
                else { usernmList.Add(relationship.UsernameOne); }
            }
            // create a list of users, and populate it with the users whose names are in usrnmlist
            List<ApplicationUser> friendList = new List<ApplicationUser>();
            foreach (string name in usernmList) { friendList.Add(getUser(name)); }
            return friendList.ToList();
        }

        public ApplicationUser getUser(string name)
        {
            //not to be used outside of this repo.
            return (from ApplicationUser user in db.Users
                    where user.UserName == name
                    select user).Single();
        }

        public bool areFriends(string friend)
        {
            string myname = System.Web.HttpContext.Current.User.Identity.Name;
            //you are your own friend.
            if (friend == myname) 
            {
                return true; 
            }
            //get myfriends
            IEnumerable<FriendshipModel> myfriends = (from FriendshipModel entry in db.FriendshipModels
                                                          where (entry.UsernameOne == myname || entry.UsernameTwo == myname)
                                                          select entry).ToList();
            // check if friend is in myfriends

            foreach(FriendshipModel friends in myfriends){
                if (friends.UsernameOne == friend || friends.UsernameTwo == friend) { return true; }
            }
            return false;

        }


        public bool addFriend(string pot_friend)
        {
            if (areFriends(pot_friend)) { return false; }
            else
            {
                string myname = System.Web.HttpContext.Current.User.Identity.Name;
                FriendshipModel friendship = new FriendshipModel();
                friendship.UsernameOne = myname;
                friendship.UsernameTwo = pot_friend;
                db.FriendshipModels.Add(friendship);
                db.SaveChanges();
                return true;
            }
        }


        public bool unFriend(string exName)
        {
            //return false if they're not friends //
            if (!areFriends(exName)) { return false; }
            else
            {
                string myname = System.Web.HttpContext.Current.User.Identity.Name;
                FriendshipModel friendship = (from FriendshipModel rel in db.FriendshipModels
                                              where (( rel.UsernameOne == myname && rel.UsernameTwo == exName) || (rel.UsernameOne == exName && rel.UsernameTwo == myname))
                                              select rel).Single();
                db.FriendshipModels.Remove(friendship);
                db.SaveChanges();
                return true;
            }
        }

        /*  PROFILE POST STUFF */

        //public IEnumerable<PostModel>;

        public IEnumerable<PostModel> getAllPostsForUser(string userID)
        {
            PostRepo pRepo = new PostRepo(db);
            IEnumerable<PostModel> posts = (from PostModel post in db.PostModels
                                            where post.userID == userID
                                            select post).ToList();

            foreach (PostModel item in posts)
            {
                item.comments = pRepo.getAllComments(item.postID).ToList();
            }



            return posts;

        }

        public bool postToProfile(PostModel post, string username){
            if(post.text == null)
            {
                return false;
            }

            db.PostModels.Add(post);
            db.SaveChanges();

            int PostId = (from PostModel fPost in db.PostModels
                          where fPost.userID == post.userID && fPost.createdDate == post.createdDate && fPost.text == post.text
                          select fPost).OrderByDescending(x => x.createdDate).Single().postID;


            ProfilePostRelationModel newBoy = new ProfilePostRelationModel();
            newBoy.postId = PostId;
            newBoy.UserId = username;

            db.ProfilePostRelationModels.Add(newBoy);
            db.SaveChanges();
            return true;
        }


        public bool removePostFromProfile(int postID)
        {

            /* MAY CAUSE ERROR CHECK OUT NOW */
            PostRepo pRepo = new PostRepo(db);
            pRepo.DeletePost(postID);

            return true;

        }

        /* GAME FAVES */

        public bool addGameToFavourites(int gameId)
        {
            if (isFave(gameId))
            {
                return false;
            }
            string myname = System.Web.HttpContext.Current.User.Identity.Name;
            FavouriteRelationModel james = new FavouriteRelationModel();
            james.gameId = gameId;
            james.username = myname;
            db.FavouriteRelationModel.Add(james);
            db.SaveChanges();
            return true;
        }

        public bool removeFromFavourites(int gameId)
        {
            if(!isFave(gameId))
            {
                return false;
            }
            string myname = System.Web.HttpContext.Current.User.Identity.Name;
            FavouriteRelationModel delRel = (from FavouriteRelationModel model in db.FavouriteRelationModel
                                             where model.gameId == gameId && model.username == myname
                                             select model).Single();

            db.FavouriteRelationModel.Remove(delRel);
            db.SaveChanges();
            return true;
        }


        bool isFave(int gameId)
        {
            string myname = System.Web.HttpContext.Current.User.Identity.Name;
            FavouriteRelationModel check = (from FavouriteRelationModel model in db.FavouriteRelationModel
                                            where model.username == myname && model.gameId == gameId
                                            select model).SingleOrDefault();

            return (check != null);
        }


        /*------------------- DISPOSE */
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}