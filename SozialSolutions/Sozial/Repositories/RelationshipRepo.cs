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


        public IEnumerable<ApplicationUser> getFriends(string username)
        {
            ApplicationUser user = (from ApplicationUser item in db.Users
                                  where item.UserName == username
                                  select item).Single();
            return user.friends;
        }


        public bool areFriends(string friend)
        {
            //get current user //
            string myname = System.Web.HttpContext.Current.User.Identity.Name;
            ApplicationUser user1 = (from ApplicationUser Iuser in db.Users
                                     where Iuser.UserName == myname
                                     select Iuser).Single();

            //get speculative friend //
            ApplicationUser user2 = (from ApplicationUser user in db.Users
                                     where user.UserName == friend
                                     select user).Single();

            // check if their friendship is one sided or "if A is friend of B, but B is not friend of A" if so. throw an exception.
            if (user2.friends.Contains(user1) ^ user1.friends.Contains(user2)) { throw new Exception("Oops, the friend-relationship between user1 and user2 is not mutual.");  } 
            // cool . return without ambiguity if they're friends or not.
            return (user1.friends.Contains(user2) && user2.friends.Contains(user1));

        }


        public bool addFriend(string pot_friend)
        {
            // return false if the users are already friends. note: this will throw an exception in areFriends if the friendship is not mutual. //
            if ( areFriends(pot_friend) ) { return false; }
            else
            {
                //get current user. //
                string myname = System.Web.HttpContext.Current.User.Identity.Name;
                ApplicationUser self = (from ApplicationUser user in db.Users
                                        where user.UserName == myname
                                        select user).Single();
                //get potential friend //
                ApplicationUser friend = (from ApplicationUser user in db.Users
                                          where user.UserName == pot_friend
                                          select user).Single();
                // add each of the users in eachothers' friendslist //
                self.friends.Add(friend);
                friend.friends.Add(self);
                //set state to modified in the database. //
                db.Entry(self).State = EntityState.Modified;
                db.Entry(friend).State = EntityState.Modified;
                //save the changes //
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
                //get myself
                string myname = System.Web.HttpContext.Current.User.Identity.Name;
                ApplicationUser self = (from ApplicationUser user in db.Users
                                        where user.UserName == myname
                                        select user).Single();

                // get exfriend //
                ApplicationUser exFriend = (from ApplicationUser user in db.Users
                                            where user.UserName == exName
                                            select user).Single();

                //remove eachother from the friends lists!
                self.friends.Remove(exFriend);
                exFriend.friends.Remove(self);
                // mark entitystate as modified in the db
                db.Entry(self).State = EntityState.Modified;
                db.Entry(exFriend).State = EntityState.Modified;
                //save the changes to the database
                db.SaveChanges();
                //return true for success
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