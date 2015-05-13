using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sozial.Models
{
    public class GameModel
    {        
        [Key]
        public int gameID { get; set; }

        [Required(ErrorMessage = "The game needs a name")]
        [Display(Name = "Game name:")]
        public string nameOfGame { get; set; }

        [Required(ErrorMessage = "Give me a description of this game please")]
        [Display(Name = "About the game:")]
        public string aboutGame { get; set; }

        [Required(ErrorMessage = "There must be a game company for this game")]
        [Display(Name = "Game Company:")]
        public string gameCompany { get; set; }

        public bool isTopTen { get; set; }  //using for testing purposes
        
        [Display(Name = "Game Genre:")]
        public string genre { get; set; }

        [Url(ErrorMessage = "Has to be a link on the web")]
        [Display(Name = "Image for the game: ")]
        public string imageUrl { get; set; }
        //Don´t Touch mystuff
        public IEnumerable<ReviewModel> gameReview;
    }
    /*
    public class ReviewModel
    {
        [Key]
        public int reviewId { get; set; }
        public DateTime dateCreated { get; set; }
        public string userId { get; set; }
        public string text { get; set; }

        public RatingModel gameRating { get; set; }
        public int likeCount { get; set; }
    }

    public class RatingModel
    {
        [Key]
        public int ratingId { get; set; }

        public int gameID { get; set; }

        public string Username { get; set; }

        public int rating { get; set; }

        public DateTime CreatedDate { get; set; }
    }
    */

}