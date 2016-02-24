namespace Neighbours.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Neighbours.Data.Common.Repositories;
    using Neighbours.Data.Models;
    using Neighbours.Services.Data.Contracts;
    using Neighbours.Data;
    using Microsoft.AspNet.Identity;
    using System.Web;
    using Microsoft.Owin.Security;
    using System.Web.Security;

    public class UsersService : IUsersService
    {
        private readonly IRepository<User> users;

        public UsersService(IRepository<User> users)
        {
            this.users = users;
        }

        public IQueryable<User> GetAll()
        {
            return this.users.All();
        }

        public User GetById(string id)
        {
            return this.users.GetById(id);
        }

        public void AddToRole(string userId, string role)
        {
            var userStore = new UserStore<User>(new NeighboursDbContext());
            var userManager = new UserManager<User>(userStore);

            var isInRole = userManager.IsInRole(userId, role);

            if (isInRole)
            {
                return;
            }

            userManager.AddToRole(userId, role);
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;

            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            var user = userManager.FindById(userId);
            var identity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

            authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);
        }
    }
}
