using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Sozial.Models;

namespace Sozial.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        /*steam*/
        public string steamId { get; set; } 
        /*steam*/

        [Display(Name = "Profile picture")]
        public string userProfilePic { get; set; }

        [Display(Name = "Profile banner")]
        public string userBannerPic { get; set; }

        /* help police */
        public virtual ICollection<ApplicationUser> friends { get; set; }
        /* help police */

        /* posts and comments by this user.*/
        public virtual ICollection<CommentModel> madeComments { get; set; }
        public virtual ICollection<PostModel> madePosts { get; set; }
        /* likes*/
        public virtual ICollection<CommentModel> likedComments { get; set; }
        public virtual ICollection<PostModel> likedPosts { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        //this is a list of favorite games that this user has favorited
        public IEnumerable<GameModel> favoriteGames { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Sozial.Models.GameModel> GameModels { get; set; }

        public System.Data.Entity.DbSet<Sozial.Models.PostModel> PostModels { get; set; }

        public System.Data.Entity.DbSet<Sozial.Models.CommentModel> CommentModels { get; set; }

        public System.Data.Entity.DbSet<Sozial.Models.NewsModel> NewsModels { get; set; }

        public System.Data.Entity.DbSet<Sozial.Models.GroupModel> GroupModels { get; set; }

        public System.Data.Entity.DbSet<Sozial.Models.FriendshipModel> FriendshipModels { get; set; }

        public System.Data.Entity.DbSet<Sozial.Models.GroupRelationshipModel> GroupRelationshipModels { get; set; }

        public System.Data.Entity.DbSet<Sozial.Models.GroupPostRelationModel> GroupPostRelationModels { get; set; }

        public System.Data.Entity.DbSet<Sozial.Models.ProfilePostRelationModel> ProfilePostRelationModels { get; set; }

        public System.Data.Entity.DbSet<Sozial.Models.PostCommentRelationModel> PostCommentRelationModels { get; set; }

        public System.Data.Entity.DbSet<Sozial.Models.FavouriteRelationModel> FavouriteRelationModel { get; set; }
        
    }
}