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

            PostModel post = (from Sozial.Models.PostModel posts in db.PostModels
                              where posts.postID == postID
                              select posts).Single();

            if (post != null) {
                post.comments = getAllComments(post.postID).ToList(); 
            }
            return post;
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

        public IEnumerable<CommentModel> getAllComments(int postID)
        {
            return (from CommentModel comment in db.CommentModels
                    where comment.postID == postID
                    select comment).ToList();
        }




        public void UpdatePost(Sozial.Models.PostModel post)
        {
            db.Entry(post).State = EntityState.Modified;
            db.SaveChanges();
        }



        /* DISPOSE */
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