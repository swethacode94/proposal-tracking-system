using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace pts2.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
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

        public System.Data.Entity.DbSet<pts2.Models.client_info> client_info { get; set; }
       
        public System.Data.Entity.DbSet<pts2.Models.enquiry_info> enquiry { get; set; }
        public System.Data.Entity.DbSet<pts2.Models.attachments_info> attachment { get; set; }
        public System.Data.Entity.DbSet<pts2.Models.country> country { get; set; }
        public System.Data.Entity.DbSet<pts2.Models.industry> industry { get; set; }

        public System.Data.Entity.DbSet<pts2.Models.proposal_info> proposal { get; set; }
        public System.Data.Entity.DbSet<pts2.Models.pros_attachments_info> pros_attachment { get; set; }

        public System.Data.Entity.DbSet<pts2.Models.invoice_main> invoice_main { get; set; }
        public System.Data.Entity.DbSet<pts2.Models.invoice_item> invoice_item { get; set; }
    }
}