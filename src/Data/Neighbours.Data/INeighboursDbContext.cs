namespace Neighbours.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using Neighbours.Data.Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    public interface INeighboursDbContext
    {
        IDbSet<User> Users { get; set; }

        IDbSet<ProfileImage> ProfileImages { get; set; }

        IDbSet<CommunityImage> CommunityImages { get; set; }

        IDbSet<IdentityUserRole> UserRoles { get; set; }

        IDbSet<PostImage> PostImages { get; set; }

        IDbSet<Community> Communities { get; set; }

        IDbSet<City> Cities { get; set; }

        IDbSet<Comment> Comments { get; set; }

        IDbSet<District> Districts { get; set; }

        IDbSet<Post> Posts { get; set; }

        DbContext DbContext { get; }

        IDbSet<T> Set<T>()
            where T : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity)
            where TEntity : class;

        void Dispose();

        int SaveChanges();
    }
}
