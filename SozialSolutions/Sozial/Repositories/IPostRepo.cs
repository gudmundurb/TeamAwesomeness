﻿using Sozial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sozial.Repositories
{
    public interface IPostRepo : IDisposable
    {
        IEnumerable<CommentModel> getCommentList();
        CommentModel getComment(int? id);

        IEnumerable<PostModel> GetPost();
        PostModel GetPostByID(int? postID);
        IEnumerable<PostModel> GetByUserID(string userID);
        void InsertPost(PostModel post);
        void DeletePost(int postID);
        void UpdatePost(PostModel post);
        //gets all comments for post
        IEnumerable<CommentModel> getAllComments(int postId);
        void deleteComment(int commentId);
        void Reply(CommentModel comment);
    }
}