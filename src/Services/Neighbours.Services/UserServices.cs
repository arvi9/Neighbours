using Neighbours.Services.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neighbours.Data.Models;
using Neighbours.Data.Common.Repositories;

namespace Neighbours.Services.Data
{
    public class UserServices : IUserServices
    {
        private readonly IRepository<User> users;

        public UserServices(IRepository<User> users)
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
    }
}
