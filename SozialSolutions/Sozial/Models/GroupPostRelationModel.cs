using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sozial.Models
{
    public class GroupPostRelationModel
    {
        [Key] public int relId { get; set; }

        public int postId { get; set; }
        public int groupId { get; set; }
    }
}