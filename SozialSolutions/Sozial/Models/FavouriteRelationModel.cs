using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Sozial.Models
{
    public class FavouriteRelationModel
    {
        [Key]
        public int relID { get; set; }

        public string username { get; set; }
        public int gameId { get; set; }

    }
}