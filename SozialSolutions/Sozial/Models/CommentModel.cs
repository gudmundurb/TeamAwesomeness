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


        private DateTime createdDate { get; set; }
    
    
/*
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyy}")]
        public DateTime CreatedDate
        {
            get { return createdDate ?? DateTime.UtcNow; }
            set { createdDate = value; }
        }

*/
        [Required(ErrorMessage = "You cannot leave an empty comment!")]
        [Display(Name = "Comment text")]
        public string commentText { get; set; }
    }
}