using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SozialProject.Models
{
    public class UserModel
    {
        [Key]
        public int userID { get; set; }

        //[Key]
        //public string steamId { get; set; }

        [Required(ErrorMessage = "You must create an username")]
        [Display(Name = "Username")]
        public string username { get; set; }

        [Display(Name = "Profile picture")]
        public string userProfilePic { get; set; }

        [Display(Name = "Profile banner")]
        public string userBannerPic { get; set; }

        public PostModel userPost { get; set; }

        public GroupModel userGroup { get; set; }

        public GameModel userGames { get; set; }

        //Laura is working on this and related files.
    }
}