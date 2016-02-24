namespace Neighbours.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Neighbours.Data.Models;
    using Neighbours.Services.Common.Contracts;

    public interface ICitiesService : IService
    {
        IQueryable<City> GetAll();

        City GetById(int id);

        City Add(City model);

        City Add(string name);
    }
}
