namespace pts2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin.Security;
    using System.Web.Helpers;
    using Microsoft.AspNet.Identity;

    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using pts2.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<pts2.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(pts2.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Roles.AddOrUpdate(r => r.Name,
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Name = "admin" });
            context.SaveChanges();

            context.Users.AddOrUpdate(r => r.UserName,
            new pts2.Models.ApplicationUser
            {
                UserName = "admin@admin.net",
                Email = "admin@admin.net",

                EmailConfirmed = true,
                PasswordHash = Crypto.HashPassword("Swetha1@2"),
                SecurityStamp = "4138b8d7-e98c-43f1-9b99-cf48b730bb5a",
                PhoneNumber = "0123456789",
                PhoneNumberConfirmed = true
            });

            context.SaveChanges();




            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            UserManager.AddToRole("4e2281fc-6da9-45ac-b623-b2ce1bd9702b", "admin");
            context.SaveChanges();
        }
    }
}
