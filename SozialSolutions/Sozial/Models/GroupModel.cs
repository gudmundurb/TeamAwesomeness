using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SozialProject.Models
{
    public class GroupModel
    {
        [Key]
        private int groupId { get; set; }

        [Key]
        private int creatorId { get; set; }


        [Required]
        [Display(Name = "Group Name")]
        public string groupName { get; set; }


        [Required]
        [Display(Name = "Group Description")]
        public string groupDescription { get; set; }


    }
}