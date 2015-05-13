using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sozial.Models
{
    public class ProfileViewModel
    {
        //list of my friends done
        public List<ApplicationUser> myFriends { get; set; }
        //List of my groups done
        public List<GroupModel> myGroups { get; set; }
        //List of my favorite games not implemented yet
        public List<GameModel> myGames { get; set; }

        //10 most recent posts from your friends and me are collected here
        public List<PostModel> newestPosts { get; set; }

        //10 most recent group posts -done
        public List<PostModel> newGroupPosts { get; set; }
    }
}