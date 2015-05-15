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

        public PostRepo()
        {
            this.db = new ApplicationDbContext();
        }


        public PostRepo(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<PostModel> GetPost()
        {
            return db.PostModels.ToList();
        }

        public IEnumerable<CommentModel> getCommentList()
        {
            return db.CommentModels.ToList();
        }

        public CommentModel getComment(int? id)
        {
            return db.CommentModels.Find(id);
        }

        public PostModel GetPostByID(int? postID)
        {
            Repositories.RelationshipRepo relRepo = new RelationshipRepo(db);
            PostModel post = (from Sozial.Models.PostModel posts in db.PostModels
                              where posts.postID == postID
                              select posts).Single();

            post.userPicture = relRepo.getUser(post.userID).userProfilePic;
            if (post != null) {
                post.comments = getAllComments(post.postID).OrderByDescending(x => x.createdDate).ToList(); 
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


            ProfilePostRelationModel profRel = (from ProfilePostRelationModel Pmodel in db.ProfilePostRelationModels
                                                where Pmodel.postId == postID
                                                select Pmodel).SingleOrDefault();

            GroupPostRelationModel grpRel = (from GroupPostRelationModel Gmodel in db.GroupPostRelationModels
                                                where Gmodel.postId == postID
                                                select Gmodel).SingleOrDefault();

                /* DELETE ANY RELATIONS */
            if (profRel != null)
            {
                db.ProfilePostRelationModels.Remove(profRel);
            }
            if (grpRel != null)
            {
                db.GroupPostRelationModels.Remove(grpRel);
            }


            IEnumerable<CommentModel> delComments = getAllComments(postID);
            CommentRepo commrepo = new CommentRepo(db);
            foreach (CommentModel model in delComments)
            {
                commrepo.deleteComment(model.commentID);
            }

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
                    select comment).OrderByDescending(x => x.createdDate).ToList();
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