using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sozial.Models
{
    public class postReplyViewModel
    {
        public int PostID { get; set; }
        public CommentModel comment {get; set; }

        public postReplyViewModel(int postId)
        {
            comment = new CommentModel();
        }

    }
}