using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

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

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
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
        /* ------------ dis has to be fixed
        public System.Data.Entity.DbSet<SozialProject.Models.UserModel> ProfileModels { get; set; }
        */
    }
}