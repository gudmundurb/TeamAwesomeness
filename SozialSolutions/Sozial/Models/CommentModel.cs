using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sozial.Models
{
    public class CommentModel
    {
        [Key]
        public int commentID { get; set; }

        public int commenterID { get; set; }

        [Required(ErrorMessage = "You cannot leave an empty comment, jees.")]
        [Display(Name = "Comment text")]
        public string commentText { get; set; }
    }
}