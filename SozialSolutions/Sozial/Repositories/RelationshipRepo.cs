﻿using System;
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

        ApplicationUser getUser(string name)
        {
            //not to be used outside of this repo.
            return (from ApplicationUser user in db.Users
                    where user.UserName == name
                    select user).Single();
        }

        public bool areFriends(string friend)
        {
            string myname = System.Web.HttpContext.Current.User.Identity.Name;
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



        /* DISPOSE */
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