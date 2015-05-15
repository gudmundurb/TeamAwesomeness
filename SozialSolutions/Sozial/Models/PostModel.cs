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

        [Required(ErrorMessage = "No text? How eloquent!")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Post Text")]
        public string text { get; set; }
        //[DataType(DataType.MultilineText)]
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
        [Url(ErrorMessage="You can currently only write the images' URL")]
        public string imageUrl { get; set; }

        public int likeCount { get; set; }

        public virtual string userPicture { get; set; }
    }
}