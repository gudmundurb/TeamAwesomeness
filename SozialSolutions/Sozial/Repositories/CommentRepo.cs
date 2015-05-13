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
            string myname = System.Web.HttpContext.Current.User.Identity.Name;
            db.CommentModels.Add(newComment);
            db.SaveChanges();

            CommentModel comment = (from CommentModel comm in db.CommentModels
                                    where comm.authorID == myname && comm.commentText == newComment.commentText
                                    select comm).OrderByDescending(x => x.createdDate).First();

            PostCommentRelationModel newReply = new PostCommentRelationModel();
            newReply.commentId = comment.commentID;
            newReply.postId = comment.postID;

            db.PostCommentRelationModels.Add(newReply);

            db.SaveChanges();
        }

        public void deleteComment(int delId)
        {
            IEnumerable<PostCommentRelationModel> rels = (from PostCommentRelationModel model in db.PostCommentRelationModels
                                                          where model.commentId == delId
                                                          select model).ToList();

            foreach (PostCommentRelationModel item in rels)
            {
                db.PostCommentRelationModels.Remove(item);
            }

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