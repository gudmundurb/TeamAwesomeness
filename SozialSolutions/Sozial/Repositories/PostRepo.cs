using Sozial.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Sozial.Repositories
{
    public class PostRepo : IPostRepo, IDisposable
    {

        private ApplicationDbContext db = null; //new ApplicationDbContext();

        public PostRepo(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<PostModel> GetPost()
        {
            return db.PostModels.ToList();
        }

        public PostModel GetPostByID(int? postID)
        {
            return db.PostModels.Find(postID);
        }

        public void InsertPost(PostModel post)
        {
            db.PostModels.Add(post);
            db.SaveChanges();
        }

        public void DeletePost(int postID)
        {
            PostModel post = db.PostModels.Find(postID);
            db.PostModels.Remove(post);
            db.SaveChanges();
        }

        public IEnumerable<PostModel> GetByUserID(string userID)
        {
            return (from PostModel jim in db.PostModels
                    where jim.userID == userID
                    select jim).ToList();
        }



        public void UpdatePost(Sozial.Models.PostModel post)
        {
            db.Entry(post).State = EntityState.Modified;
            db.SaveChanges();
        }

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