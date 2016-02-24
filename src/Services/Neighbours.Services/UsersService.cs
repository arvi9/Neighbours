namespace Neighbours.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Security;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin.Security;
    using Neighbours.Data;
    using Neighbours.Data.Common.Repositories;
    using Neighbours.Data.Models;
    using Neighbours.Services.Data.Contracts;

    public class UsersService : IUsersService
    {
        private readonly IRepository<User> users;
        private readonly IRepository<Community> communities;

        public UsersService(IRepository<User> users, IRepository<Community> communities)
        {
            this.users = users;
            this.communities = communities;

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

        public IQueryable<User> GetAllPending(int id)
        {
            var users = new List<User>();

            var usersList = this.users.All();

            foreach (var user in usersList)
            {
                if (user.WaitingListCommunities.Contains(id))
                {
                    users.Add(user);
                }
            }

            return users.AsQueryable();
        }

        public void AddCommunityToWait(string userId, int id)
        {
            var user = this.users.GetById(userId);

            var list = user.WaitingListCommunities.ToList();

            list.Add(id);

            user.WaitingListCommunities = list.ToArray();

            this.users.SaveChanges();

        }

        public void AddToCommunity(string userId, int id)
        {
            var community = this.communities.GetById(id);
            var user = this.users.GetById(userId);

            user.Communities.Add(community);
            community.Users.Add(user);

            var list = user.WaitingListCommunities.ToList();

            list.Remove(id);

            user.WaitingListCommunities = list.ToArray();

            var usersRemoveFromWaiting = community.WaitingUsersList.ToList();

            usersRemoveFromWaiting.Remove(userId);

            community.WaitingUsersList = usersRemoveFromWaiting.ToArray();

            community.Users.Add(user);

            this.users.SaveChanges();
            this.communities.SaveChanges();
        }

        public void Deny(string userId, int id)
        {
            var community = this.communities.GetById(id);
            var user = this.users.GetById(userId);

            var list = user.WaitingListCommunities.ToList();

            list.Remove(id);

            user.WaitingListCommunities = list.ToArray();
        }
    }
}
