using Neighbours.Data.Models;
using Neighbours.Services.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neighbours.Services.Data.Contracts
{
    public interface IUserServices : IService
    {
        IQueryable<User> GetAll();

        User GetById(string id);
    }
}
