using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sozial.Models
{
    public class PostToProfileViewModel
    {
        public string profileOwner { get; set; }
        public PostModel newPost { get; set; }
    }
}