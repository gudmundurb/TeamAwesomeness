using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sozial.Models
{
    public class ProfileViewModel
    {
        //list of my friends
        public IEnumerable<ApplicationUser> myFriends { get; set; }
        //List of my groups
        public IEnumerable<GroupModel> myGroups { get; set; }
        //List of my favorite games
        public IEnumerable<GameModel> myGames { get; set; }
    }
}