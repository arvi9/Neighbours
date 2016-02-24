namespace Neighbours.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Neighbours.Data.Common.Models;
    using Neighbours.Data.Models;

    public class NeighboursDbContext : IdentityDbContext<User>, INeighboursDbContext
    {
        public NeighboursDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbContext DbContext => this;

        public virtual IDbSet<ProfileImage> ProfileImages { get; set; }

        public virtual IDbSet<CommunityImage> CommunityImages { get; set; }

        public virtual IDbSet<IdentityUserRole> UserRoles { get; set; }

        public virtual IDbSet<PostImage> PostImages { get; set; }

        public virtual IDbSet<Community> Communities { get; set; }

        public virtual IDbSet<City> Cities { get; set; }

        public virtual IDbSet<Comment> Comments { get; set; }

        public virtual IDbSet<District> Districts { get; set; }

        public virtual IDbSet<Post> Posts { get; set; }

        public static NeighboursDbContext Create()
        {
            return new NeighboursDbContext();
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            this.ApplyDeletableEntityRules();
            return base.SaveChanges();
        }

        public new IDbSet<T> Set<T>()
            where T : class
        {
            return base.Set<T>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Community>().Property(c => c.Latitude).HasPrecision
            base.OnModelCreating(modelBuilder);
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    if (!entity.PreserveCreatedOn)
                    {
                        entity.CreatedOn = DateTime.Now;
                    }
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        private void ApplyDeletableEntityRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (
                var entry in
                    this.ChangeTracker.Entries()
                        .Where(e => e.Entity is IDeletableEntity && (e.State == EntityState.Deleted)))
            {
                var entity = (IDeletableEntity)entry.Entity;

                entity.DeletedOn = DateTime.Now;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }
    }
}
