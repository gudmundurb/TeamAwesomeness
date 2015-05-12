using Sozial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sozial.Repositories
{
    public interface IGroupRepo : IDisposable
    {
        //gets all members of group id.
        IEnumerable<ApplicationUser> getMembers(int groupid);

        //returns true if user is in group id.
        bool inGroup(int groupID);

        //current user is inducted into group id
        bool joinGroup(int groupID);

        // current user is removed from group id
        bool leaveGroup(int groupID);

        // returns n recently created groups.
        IEnumerable<GroupModel> getRecentGroups(int n);

        /* posts */

        IEnumerable<PostModel> getAllPostsForGroup(int groupId);

        bool addPostToGroup(PostModel post, int groupId);
        bool removePostFromGroup(int postid);
        
        
    }
}