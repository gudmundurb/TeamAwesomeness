using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Sozial.Models;

namespace Sozial.Models
{
    public class NewsModel
    {
        public NewsModel()
        {
            createdDate = DateTime.Now;
        }
        //Constructor with a user id
        public NewsModel(string text)
        {
            this.userID = userID;
            this.text = text;
            createdDate = DateTime.Now;
        }

        [Key]
        public int newsID { get; set; }
    
        public string userID { get; set; } // the user that made the news.

        [Required(ErrorMessage = "Title is needed")]
        [Display(Name = "Title: ")]
        public string title { get; set; }
        
        [Required(ErrorMessage = "Text is needed")]
        [Display(Name = "News: ")]
        [DataType(DataType.MultilineText)]
        public string text { get; set; }

        private DateTime createdDate { get; set; }
        /*
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyy}")]
        public DateTime CreatedDate
        {
            get { return createdDate ?? DateTime.UtcNow; }
            set { createdDate = value; }
        }
        */
        [Url(ErrorMessage="Has to be a link on the web")]
        public string imageUrl { get; set; }
    }
}