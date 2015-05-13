using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sozial.Models
{
    public class ReviewModel
    {
        public RatingModel gameRating { get; set; }
        public int likeCount { get; set; }
    }
}