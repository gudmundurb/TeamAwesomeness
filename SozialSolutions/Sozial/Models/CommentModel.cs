using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sozial.Models
{
    public class CommentModel
    {
        //constructor
        public CommentModel (string ApplicationUser, int postID)
        {
            authorID = ApplicationUser;
            this.postID = postID;
        }
        [Key]
        public int commentID { get; set; }

        public string authorID { get; set; }

        public int postID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyy}")]
        public DateTime createDate { get; set; }


        [Required(ErrorMessage = "You cannot leave an empty comment, jees.")]
        [Display(Name = "Comment text")]
        public string commentText { get; set; }
    }
}