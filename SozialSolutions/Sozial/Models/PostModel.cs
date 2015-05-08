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
        [Key]
        public int postID { get; set; }

        
        public int userID { get; set; } // the user that made the comment.


        [Required(ErrorMessage = "You cant leave an empty comment. Have some manners!")]
        [Display(Name = "Post Text")]
        public string postText;

        public virtual ICollection<CommentModel> comments { get; set; }
    }
}