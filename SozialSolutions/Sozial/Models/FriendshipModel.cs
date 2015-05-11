using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sozial.Models
{
    public class FriendshipModel
    {
        [Key] public int friendshipID { get; set; }

        public string UsernameOne { get; set; }
        public string UsernameTwo { get; set; }
    }
}