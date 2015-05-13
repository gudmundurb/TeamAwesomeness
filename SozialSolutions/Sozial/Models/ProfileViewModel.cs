using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sozial.Models
{
    public class ProfileViewModel
    {
        //list of my friends
        public List<ApplicationUser> myFriends { get; set; }
        //List of my groups
        public List<GroupModel> myGroups { get; set; }
        //List of my favorite games
        public List<GameModel> myGames { get; set; }

        //5 most recent posts from your friends and me are collected here
        public List<PostModel> newestPosts { get; set; }

        //5 most recent group posts
        public List<PostModel> newGroupPosts { get; set; }
    }
}