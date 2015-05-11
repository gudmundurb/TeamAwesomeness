using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sozial.Models
{
    public class GroupRelationshipModel
    {
        [Key]
        public int GroupRelID { get; set; }

        public int GroupID { get; set; }
        public string Username { get; set; }
    }
}