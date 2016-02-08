namespace Neighbours.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<Neighbours.Data.NeighboursDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Neighbours.Data.NeighboursDbContext context)
        {
            //if (!context.Users.Any())
            //{
            //    var roleStore = new RoleStore<IdentityRole>(context);
            //    var roleManager = new RoleManager<IdentityRole>(roleStore);
            //    var userStore = new UserStore<User>(context);
            //    var userManager = new UserManager<User>(userStore);
            //    userManager.PasswordValidator = new PasswordValidator()
            //    {
            //        RequiredLength = 5,
            //    };

            //    // Add missing roles
            //    var roleAdmin = roleManager.FindByName("Administrator");
            //    if (roleAdmin == null)
            //    {
            //        roleAdmin = new IdentityRole("Administrator");
            //        roleManager.Create(roleAdmin);
            //    }



            //    // Create test users
            //    var userAdmin = userManager.FindByEmail("admin@site.com");
            //    if (userAdmin == null)
            //    {
            //        var newUser = new AppUser()
            //        {
            //            Email = "admin@site.com",
            //            FirstName = "Admin",
            //            LastName = "Admin",
            //            UserName = "admin@site.com",
            //            ImageUrl = "http://41.media.tumblr.com/tumblr_m9afi3NIYO1qhuriwo1_500.jpg"
            //        };

            //        userManager.Create(newUser, "admin");
            //        //userManager.SetLockoutEnabled(newUser.Id, true);
            //        userManager.AddToRole(newUser.Id, "Administrator");
            //    }
            //}
        }
    }
}
