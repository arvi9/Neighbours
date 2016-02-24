namespace Neighbours.Services.Data
{
    using System;
    using System.Linq;
    using Common.Contracts;
    using Contracts;
    using Neighbours.Data.Common.Repositories;
    using Neighbours.Data.Models;

    public class CommunitiesService : ICommunitiesService
    {
        private IDeletableEntityRepository<Community> communities;
        private IDistrictsService districts;
        private ICitiesService cities;
        private IRepository<User> users;
        private IIdentifierProvider provider;

        public CommunitiesService(IDeletableEntityRepository<Community> communities, IDistrictsService districts, ICitiesService cities, IRepository<User> users, IIdentifierProvider provider)
        {
            this.communities = communities;
            this.districts = districts;
            this.cities = cities;
            this.users = users;
            this.provider = provider;
        }

        public void Add(Community model, string city, string district, string userId)
        {
            var districtToAdd = this.districts.Add(district);
            var cityToAdd = this.cities.Add(city);

            var comm = new Community()
            {
                Address = model.Address,
                City = cityToAdd,
                District = districtToAdd,
                Name = model.Name,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                Status = Status.Pending,
                CommunityImageId = model.CommunityImageId,
                CreatorId = userId,
                CreatedOn = DateTime.UtcNow,
            };

            var user = this.users.GetById(userId);
            comm.Admins.Add(user);
            comm.Users.Add(user);

            this.communities.Add(comm);
            this.communities.SaveChanges();
        }

        public IQueryable<Community> GetAll()
        {
            return this.communities.All();
        }

        public Community GetById(int id)
        {
            return this.communities.GetById(id);
        }

        public Community Details(string id)
        {
            var intId = this.provider.DecodeId(id);

            return this.communities.GetById(intId);
        }

        public void Join(string userId, int communityId)
        {
            var user = this.users.GetById(userId);

            var communityToJoin = this.communities.GetById(communityId);

            if (communityToJoin.WaitingUsers == null)
            {
                communityToJoin.WaitingUsersList = new string[0];
            }

            var waitingList = communityToJoin.WaitingUsersList;

            var list = waitingList.ToList();

            list.Add(userId);

            communityToJoin.WaitingUsersList = list.ToArray();

            this.communities.SaveChanges();
        }

        public void Cancel(string userId, int communityId)
        {
            var user = this.users.GetById(userId);

            var communityToJoin = this.communities.GetById(communityId);

            var waitingList = communityToJoin.WaitingUsersList;

            var userListWaiting = user.WaitingListCommunities.ToList();
            userListWaiting.Remove(communityToJoin.Id);

            user.WaitingListCommunities = userListWaiting.ToArray();
            var list = waitingList.ToList();

            list.Remove(userId);

            communityToJoin.WaitingUsersList = list.ToArray();

            this.communities.SaveChanges();
            this.users.SaveChanges();
        }

        public void Leave(string userId, int communityId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Community> GetAllMine(string userId)
        {
            return this.communities.All()
                .Where(c => c.CreatorId == userId || c.Users.FirstOrDefault(x => x.Id == userId) != null);
        }

        public void UpdateName(int id, string name)
        {
            var community = this.communities.GetById(id);

            community.Name = name;
            this.communities.SaveChanges();
        }

        public void Delete(int id)
        {
            this.communities.Delete(id);
            this.communities.SaveChanges();
        }
    }
}
