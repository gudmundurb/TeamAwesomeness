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
            //userID = ;

        }
        //Constructor with a user id
        public PostModel(string ApplicationUser)
        {
            userID = ApplicationUser;

        }

        [Key]
        public int postID { get; set; }
    
        public string userID { get; set; } // the user that made the comment.

        [Required(ErrorMessage = "No text for a post eh... that is interesting 'post' I would say :) !")]
        [Display(Name = "Post Text")]
        public string text { get; set; }

        public ICollection<CommentModel> comments { get; set; } //this instead of many to many relations


        private DateTime? createdDate;

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyy}")]
        public DateTime CreatedDate
        {
            get { return createdDate ?? DateTime.UtcNow; }
            set { createdDate = value; }
        }

        public string imageFile { get; set; }
    }
}