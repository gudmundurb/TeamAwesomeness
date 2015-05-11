using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sozial.Models
{
    public class FrontPageViewModel
    {

        //5 most recent posts from your friends are collected here
        public List<PostModel> newestPosts { get;set;}

        //5 most recent group posts
        public List<PostModel> newGroupPosts { get; set; }

         //5 most recent groups are collected here
        public List<GroupModel> newestGroups { get;set;}

        //5 new games are listed here
        public List<GameModel> newestGames { get; set; }

        //5 top games are listed here
        // public List<GameModel> NewestUsers { get; set; }
        
        //5 top news
        public List<NewsModel> newestNews { get; set; }
    }
}