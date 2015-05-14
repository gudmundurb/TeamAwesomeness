using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sozial.Models
{
    public class ReviewModel
    {
        [Key]
        public int reviewId { get; set; }
        public DateTime  dateCreated { get; set; }
        public string userId { get; set; }
        public string text { get; set; }

        //public IEnumerable<PostModel> reviewPost;

        public RatingModel gameRating { get; set; }
        public int likeCount { get; set; }
    }
}