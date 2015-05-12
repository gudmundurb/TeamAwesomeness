using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sozial.Models
{
    public class ProfilePostRelationModel
    {
        [Key]
        public int relId { get; set; }

        public string UserId { get; set; }
        public int postId { get; set; }
    }
}