namespace Neighbours.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public sealed class Configuration : DbMigrationsConfiguration<Neighbours.Data.NeighboursDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Neighbours.Data.NeighboursDbContext context)
        {
            if (!context.Users.Any())
            {
                var maleImage = new ProfileImage()
                {
                    FileExtension = "png",
                    OriginalFileName = "male.png",
                    NewFileName = "male.png",
                    UrlPath = "~/Content/imgs",
                };

                context.ProfileImages.Add(maleImage);

                var femaleImage = new ProfileImage()
                {
                    FileExtension = "png",
                    OriginalFileName = "female.png",
                    NewFileName = "female.png",
                    UrlPath = "~/Content/imgs",
                };

                context.ProfileImages.Add(femaleImage);

                var otherImage = new ProfileImage()
                {
                    FileExtension = "png",
                    OriginalFileName = "other.png",
                    NewFileName = "other.png",
                    UrlPath = "~/Content/imgs",
                };

                context.ProfileImages.Add(otherImage);

                var coverImage = new CommunityImage()
                {
                    FileExtension = "jpg",
                    OriginalFileName = "default-cover.png",
                    NewFileName = "default-cover.png",
                    UrlPath = "~/Content/imgs",
                };

                context.SaveChanges();

                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);
                userManager.PasswordValidator = new PasswordValidator()
                {
                    RequiredLength = 5,
                };

                // Add missing roles
                var roleSuper = roleManager.FindByName("SuperUser");
                if (roleSuper == null)
                {
                    roleSuper = new IdentityRole("SuperUser");
                    roleManager.Create(roleSuper);
                }

                var roleAdmin = roleManager.FindByName("Administrator");
                if (roleAdmin == null)
                {
                    roleAdmin = new IdentityRole("Administrator");
                    roleManager.Create(roleAdmin);
                }

                var roleUser = roleManager.FindByName("User");
                if (roleUser == null)
                {
                    roleUser = new IdentityRole("User");
                    roleManager.Create(roleUser);
                }

                // Create test users
                var userSuper = userManager.FindByEmail("super@site.com");
                if (userSuper == null)
                {
                    var newSuper = new User()
                    {
                        Email = "super@site.com",
                        FirstName = "Super",
                        LastName = "Superov",
                        UserName = "super@site.com",
                        Gender = Gender.Other,
                        BirthDate = new DateTime(1980, 1, 1),
                        ProfileImageId = 1
                    };

                    userManager.Create(newSuper, "super");
                    userManager.AddToRole(newSuper.Id, "SuperUser");
                }

                var userAdmin = userManager.FindByEmail("admin@site.com");
                if (userAdmin == null)
                {
                    var newAdmin = new User()
                    {
                        Email = "admin@site.com",
                        FirstName = "Admin",
                        LastName = "Adminov",
                        UserName = "admin@site.com",
                        Gender = Gender.Other,
                        BirthDate = new DateTime(1980, 1, 1),
                        ProfileImageId = 2
                    };

                    userManager.Create(newAdmin, "admin");
                    userManager.AddToRole(newAdmin.Id, "Administrator");
                }

                var user = userManager.FindByEmail("user1@site.com");
                if (user == null)
                {
                    var newAdmin = new User()
                    {
                        Email = "user1@site.com",
                        FirstName = "User",
                        LastName = "Userov",
                        UserName = "user1@site.com",
                        Gender = Gender.Male,
                        BirthDate = new DateTime(1980, 1, 1),
                        ProfileImageId = 3
                    };

                    userManager.Create(newAdmin, "user1");
                    userManager.AddToRole(newAdmin.Id, "User");
                }
            }
        }
    }
}
