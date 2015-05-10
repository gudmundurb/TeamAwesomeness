using Sozial.Models;
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
        void InsertPost(PostModel post);
        void DeletePost(int postID);
        void UpdatePost(PostModel post);
    }
}