using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Sozial.Models;
namespace SozialProject.Models
{
    public class PostModel
    {

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
        public string postText;

        public ICollection<CommentModel> comments { get; set; } //this instead of many to many relations

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyy}")]
        public DateTime showDate { get; set; }

        public string imageFile { get; set; }
    }
}