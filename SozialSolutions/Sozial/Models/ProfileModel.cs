using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SozialProject.Models
{
    public class ProfileModel
    {
        [Key]
        public int userID { get; set; }

        //[Key]
        public string steamId { get; set; }

        [Required(ErrorMessage = "Give me a name")]
        [Display(Name = "Name of user")]
        public string userName { get; set; }

        [Display(Name = "Picture of user")]
        public string userProfilePic { get; set; }

        [Display(Name = "Banner for user profile")]
        public string userBannerPic { get; set; }
    }
}