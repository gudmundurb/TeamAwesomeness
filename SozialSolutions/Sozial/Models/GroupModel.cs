using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Sozial.Models;
namespace Sozial.Models
{
    public class GroupModel
    {
        GroupModel(string userId, string groupNm, string groupDescr)
        {
            creatorID = userId;
            groupName = groupName;
            groupDescription = groupDescr;
        }

        [Key]
        private int groupId { get; set; }

        //foreign key for creatorID
        //public int userID { get; set; }
        //
        //[ForeignKey("creatorID")]
        private string creatorID { get; set; }


        [Required(ErrorMessage = "The group with no name already exists.")]
        [Display(Name = "Group Name")]
        public string groupName { get; set; }


        [Required(ErrorMessage = "What is your group all about?")]
        [Display(Name = "Group Description")]
        public string groupDescription { get; set; }

        [Display(Name = "Group Picture")]
        [Url(ErrorMessage = "Please Insert a valid Url.")]
        public string groupPicture { get; set; }

        [Display(Name = "Group Banner")]
        [Url(ErrorMessage = "Please Insert a valid Url.")]
        public string groupBanner { get; set; }

        public virtual ICollection<ApplicationUser> Members { get; set; }
        public virtual ICollection<PostModel> Posts { get; set; } 


    }
}