using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace SozialProject.Models
{
    public class PostModel
    {
        [Key]
        public int postID { get; set; }

        //[Key]
        public int userID { get; set; } // the user that made the comment.
        

        [Required]
        [Display(Name = "Comment text")]
        public string commentText;
    }
}