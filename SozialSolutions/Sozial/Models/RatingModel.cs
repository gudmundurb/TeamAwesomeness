using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sozial.Models
{
    public class RatingModel
    {
        [Key]
        public int ratingId { get; set; }
        public int MovieId { get; set; }
        public string Username { get; set; }
        public int rating { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}