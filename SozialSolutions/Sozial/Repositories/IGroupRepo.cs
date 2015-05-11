using Sozial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sozial.Repositories
{
    public interface IGroupRepo : IDisposable
    {
        IEnumerable<ApplicationUser> getMembers(int groupid);

        bool inGroup(int groupID);
        bool joinGroup(int groupID);
        bool leaveGroup(int groupID);
        
    }
}