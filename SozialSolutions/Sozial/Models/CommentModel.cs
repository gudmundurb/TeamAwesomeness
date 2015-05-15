using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sozial.Models
{
    public class CommentModel
    {
        public CommentModel()
        {
            createdDate = DateTime.Now;
        }
        //the cow goes moo

        public CommentModel(int postId)
        {
            this.postID = postId;
        }

        //constructor
        public CommentModel (string text, int postID)
        {
            this.postID = postID;
            commentText = text;
        }
        [Key]
        public int commentID { get; set; }

        public string authorID { get; set; }

        public int postID { get; set; }


        public DateTime createdDate { get; set; }
    
   
        [Required(ErrorMessage = "You cannot leave an empty comment!")]
        [Display(Name = "Comment text")]
        [DataType(DataType.MultilineText)]
        public string commentText { get; set; }
    }
}