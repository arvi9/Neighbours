namespace Neighbours.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Neighbours.Data.Models;
    using Neighbours.Services.Common.Contracts;

    public interface IUsersService : IService
    {
        IQueryable<User> GetAll();

        User GetById(string id);

        void AddToRole(string userId, string role);
    }
}
