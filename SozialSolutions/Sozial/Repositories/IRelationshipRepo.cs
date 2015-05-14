using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sozial.Models;
namespace Sozial.Repositories
{
    public interface IRelationshipRepo : IDisposable
    {
        //deteremines if a username represents a user in our database
        bool realUser(string username);

        // gets a list of users from usernames' friendslist.
        IEnumerable<ApplicationUser> getFriends(string username);

        //Returns true if current user is friends with username Throws if (A is friend of B, but B is not friend of A) or vice versa;
        bool areFriends(string userName);

        //returns false if current user and username are friends already, otherwise returns true
        bool addFriend(string userName);

        // returns false if current user is not friends with username, true if unfriending was successful
        bool unFriend(string exFriend);

        /* profilepost stuff */
        IEnumerable<PostModel> getAllPostsForUser(string user);

        bool postToProfile(PostModel post, string profileOwner);
        bool removePostFromProfile(int postID);
        ApplicationUser getUser(string name);


        /*gameFAVES */ 
        bool addGameToFavourites(int gameId);
        bool removeFromFavourites(int gameId);
        
    }
}