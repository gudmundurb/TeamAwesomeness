using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sozial.Models;

namespace Sozial.Repositories
{
    public interface ICommentRepo : IDisposable
    {
        /*getters. */
        IEnumerable<CommentModel> getComments();
        IEnumerable<CommentModel> getComments(int postId);

        /* C(r)UDs */

        void insertComment(CommentModel comment);
        void deleteComment(int commentID);
        void updateComment(CommentModel comment);

    }
}