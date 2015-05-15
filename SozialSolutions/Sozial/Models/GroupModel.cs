using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Sozial.Models;

namespace Sozial.Models
{
    public class GroupModel
    {
        public GroupModel()
        {
            createdDate = DateTime.Now;
        }
        
        public GroupModel(string userID, string groupNm, string groupDescr)
        {
            creatorID = userID;
            groupName =  groupNm;
            groupDescription = groupDescr;
            createdDate = DateTime.Now;
        }
        
        [Key]
        public int groupID { get; set; }

        //[ForeignKey("creatorID")]
        private string creatorID { get; set; }
        public DateTime createdDate { get; set; }

        public int likeCount { get; set; }

        [Required(ErrorMessage = "You have not filled out the name of the group!")]
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

        public bool isRecruiting { get; set; }
    }
}