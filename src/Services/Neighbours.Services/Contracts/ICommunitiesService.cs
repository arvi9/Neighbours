namespace Neighbours.Services.Data.Contracts
{
    using System.Linq;
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

        IQueryable<Community> GetAllMine(string userId);

        void UpdateName(int id, string name);

        void Delete(int id);
    }
}
