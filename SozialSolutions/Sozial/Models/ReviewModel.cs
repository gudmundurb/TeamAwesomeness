﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sozial.Models
{
    public class ReviewModel
    {
        public DateTime  dateCreated { get; set; }
        public string userId { get; set; }
        public string text { get; set; }

        public RatingModel gameRating { get; set; }
        public int likeCount { get; set; }
    }
}