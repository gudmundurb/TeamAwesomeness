﻿using Sozial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sozial.Repositories
{
    public class GroupRepo : IGroupRepo, IDisposable
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public GroupRepo(ApplicationDbContext databaseContext)
        {
            db = databaseContext;
        }


        public IEnumerable<ApplicationUser> getMembers(int groupId)
        {
            // get a list of members' usernames.
            IEnumerable<string> memberlist = (from GroupRelationshipModel rel in db.GroupRelationshipModels
                                              where rel.GroupID == groupId
                                              select rel.Username).ToList();
            
            //create a new empty list of user.
            List<ApplicationUser> members = new List<ApplicationUser>();

            //populate the list with the getUser Function. if this throws, the user either didn´t exist or was recently deleted.
            foreach (string username in memberlist)
            {
                members.Add(getUser(username));
            }
            //return the members as a ienumerable object.
            return members.ToList();
        }


        ApplicationUser getUser(string username)
        {
            return (from ApplicationUser user in db.Users
                    where user.UserName == username
                    select user).Single();
        }


        public bool inGroup(int groupId)
        {
            if (!realGroup(groupId)) { return false; }

            // get current users' name.
            string myname = System.Web.HttpContext.Current.User.Identity.Name;

            //get a single or null instance of a relationship between group with id == group id and username == myname
            GroupRelationshipModel relationship = (from GroupRelationshipModel rel in db.GroupRelationshipModels
                                                   where (rel.GroupID == groupId && rel.Username == myname)
                                                   select rel).SingleOrDefault();
            // if relationship is null return false.
            return (relationship != null);
        }

        bool realGroup(int groupId)
        {
            // find a group that has the id groupId
            GroupModel temp = (from GroupModel Group in db.GroupModels
                               where Group.groupID == groupId
                               select Group).SingleOrDefault();

            //if teh linq found no group with that id. temp will be null...
            // return false if temp is null, true otherwize
            return (temp != null);
        }

        public bool joinGroup(int groupId)
        {
            if (inGroup(groupId) || !realGroup(groupId) ) { return false; }
            else
            {
                // make a new relationship
                GroupRelationshipModel newRelationship = new GroupRelationshipModel();

                //assign values to the proper properties.
                newRelationship.Username = System.Web.HttpContext.Current.User.Identity.Name;
                newRelationship.GroupID = groupId;

                //add into the database, the new relationship, you must
                db.GroupRelationshipModels.Add(newRelationship);

                // save the new addition
                db.SaveChanges();

                // because success return true
                return true;
            }
        }


        public bool leaveGroup(int groupId)
        {
            if (!inGroup(groupId) || !realGroup(groupId)) { return false; }
            else
            {
                
                string myname = System.Web.HttpContext.Current.User.Identity.Name;

                //find the grouprelationship instance in the database.
                GroupRelationshipModel relationship = (from GroupRelationshipModel rel in db.GroupRelationshipModels
                                                       where rel.GroupID == groupId && rel.Username == myname
                                                       select rel).Single();

                //remove the relationship from the database.
                db.GroupRelationshipModels.Remove(relationship);
                db.SaveChanges();
                return true;
            }
        }


        /* dispose stuff */
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