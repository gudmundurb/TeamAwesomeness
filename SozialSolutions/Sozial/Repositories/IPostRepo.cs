﻿using Sozial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sozial.Repositories
{
    public interface IPostRepo : IDisposable
    {

        IEnumerable<PostModel> GetPost();
        PostModel GetPostByID(int? postID);
        IEnumerable<PostModel> GetByUserID(string userID);
        void InsertPost(PostModel post);
        void DeletePost(int postID);
        void UpdatePost(PostModel post);
        IEnumerable<CommentModel> getAllComments(int id);
    }
}