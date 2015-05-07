using Sozial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sozial.Repositories
{
    public class UserRepo
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void Save()
        {
            //context.SaveChanges();
        }
    }
   
}