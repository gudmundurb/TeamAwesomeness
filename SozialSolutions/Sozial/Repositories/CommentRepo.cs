using System;
using System.Collections.Generic;

using System.Data.Entity;
using System.Linq;
using System.Web;
using Sozial.Models;
namespace Sozial.Repositories
{
    public class CommentRepo : ICommentRepo, IDisposable
    {
        public ApplicationDbContext db = null;

        public CommentRepo(ApplicationDbContext database)
        {
            db = database;
        }

        public IEnumerable<CommentModel> getComments(){
            return db.CommentModels.ToList();
        }

        public IEnumerable<CommentModel> getComments(int postID)
        {
            IEnumerable<CommentModel> coll = from comments in db.CommentModels
                                              where comments.postID == postID
                                              select comments;
            return coll;
        }

        /* setters */

        public void insertComment(CommentModel newComment)
        {
            ///* TODO: IMPLEMENT BETTER TO WORK WITH POSTREPO
            PostModel post = db.PostModels.Find(newComment.postID);
            if (post == null) { 
                throw new Exception("Post returns null. post was either recently removed or there's an error. Sorry" ,
                    new ArgumentNullException() );  
            }
            db.CommentModels.Add(newComment);

            post.comments.Add(newComment);
            PostRepo postrepo = new PostRepo(db);
            postrepo.UpdatePost(post);

            
            db.SaveChanges();
        }

        public void deleteComment(int delId)
        {
            CommentModel comment = db.CommentModels.Find(delId);
            db.CommentModels.Remove(comment);
            db.SaveChanges();
        }

        public void updateComment(CommentModel comment)
        {
            db.Entry(comment).State = EntityState.Modified;
            db.SaveChanges();
        }

        /* dispose stuff ~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~-~ */
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