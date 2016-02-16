namespace Neighbours.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Neighbours.Data.Models;
    using Neighbours.Services.Common.Contracts;

    public interface IUserServices : IService
    {
        IQueryable<User> GetAll();

        User GetById(string id);
    }
}
