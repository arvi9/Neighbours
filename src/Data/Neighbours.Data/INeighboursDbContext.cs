using Neighbours.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neighbours.Data
{
    public interface INeighboursDbContext
    {
        IDbSet<T> Set<T>() where T : class;

        IDbSet<User> Users { get; set; }

        DbContext DbContext { get; }

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        void Dispose();

        int SaveChanges();
    }
}
