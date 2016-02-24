namespace Neighbours.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Common.Contracts;
    using Neighbours.Data.Models;

    public interface ICommunitiesService : IService
    {
        IQueryable<Community> GetAll();

        Community GetById(int id);

        void Add(Community model, string city, string district, string userId);

        Community Details(string id);

        void Join(string userId, int communityId);

        void Cancel(string userId, int communityId);

        void Leave(string userId, int communityId);
    }
}
