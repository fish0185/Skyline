using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyline.Data.Concrete
{
    using Skyline.Data.Configuration;
    using Skyline.Data.Entities;
    using Skyline.Data.Infrastructure;

    public class SkylineDbContext : DbContext
    {
        public DbSet<News> News { get; set; }

        public SkylineDbContext()
            : base("SkylineDb")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new NewsConfiguration());
        }

        public override int SaveChanges()
        {
            {
                foreach (var history in
                    this.ChangeTracker.Entries()
                        .Where(
                            e =>
                            e.Entity is IModificationHistory
                            && (e.State == EntityState.Added || e.State == EntityState.Modified))
                        .Select(e => e.Entity as IModificationHistory))
                {
                    history.DateModified = DateTime.Now;
                    if (history.DateCreated == DateTime.MinValue)
                    {
                        history.DateCreated = DateTime.Now;
                    }
                }

                int result = base.SaveChanges();
                foreach (var history in
                    this.ChangeTracker.Entries()
                        .Where(e => e.Entity is IModificationHistory)
                        .Select(e => e.Entity as IModificationHistory))
                {
                    history.IsDirty = false;
                }

                return result;
            }
        }
    }
}
