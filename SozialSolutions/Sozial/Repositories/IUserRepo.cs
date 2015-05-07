using SozialProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozial.Repositories
{
    public interface IUserRepo
    {
        IEnumerable<UserModel> getUser();
        UserModel getUserByID(int userID);
        void createAccount(UserModel user);
        void updateUserDetails(UserModel user);
        void Save();
    }
}
