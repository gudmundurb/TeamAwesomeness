using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Sozial.Models;

namespace Sozial.Models
{
    public class PostModel
    {
        public PostModel()
        {
            createdDate = DateTime.Now;
        }
        //Constructor with a user id
        public PostModel(string text)
        {
            this.userID = userID;
            this.text = text;
            createdDate = DateTime.Now;
        }

        [Key]
        public int postID { get; set; }
    
        public string userID { get; set; } // the user that made the comment.

        [Required(ErrorMessage = "No text for a post eh... that is interesting 'post' I would say :) !")]
        [Display(Name = "Post Text")]
        [DataType(DataType.MultilineText)]
        public string text { get; set; }

        public ICollection<CommentModel> comments { get; set; } //this instead of many to many relations

        public DateTime createdDate { get; set; }
        /*
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyy}")]
        public DateTime CreatedDate
        {
            get { return createdDate ?? DateTime.UtcNow; }
            set { createdDate = value; }
        }
        */
        [Url(ErrorMessage="Has to be a link on the web")]
        public string imageUrl { get; set; }

        public int likeCount { get; set; }
    }
}