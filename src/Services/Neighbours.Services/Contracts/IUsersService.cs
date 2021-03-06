﻿namespace Neighbours.Services.Data.Contracts
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

        void Delete(string userId);

        void Update(string userId, string username, string firstName, string lastName);

        User GetById(string id);

        void AddToRole(string userId, string role);

        IQueryable<User> GetAllPending(int id);

        void AddCommunityToWait(string userId, int id);

        void AddToCommunity(string userId, int id);

        void Deny(string userId, int id);
    }
}
