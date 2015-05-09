using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sozial.Models;

namespace Sozial.Repositories
{
    public class ICommentRepo
    {
        /*getters. */
        IEnumerable<CommentModel> getComments();
        IEnumerable<CommentModel> getComments(int postId);

        /* creates */

        void insertComment(CommentModel comment);
        void deleteComment(int commentID);
        void updateComment(CommentModel comment);



    }
}