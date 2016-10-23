using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyline.Data.Concrete
{
    using System.Collections.Specialized;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Skyline.Data.Configuration;
    using Skyline.Data.Entities;
    using Skyline.Data.Infrastructure;

    public class SkylineDbContext : IdentityDbContext<SkylineUser>
    {
        public DbSet<News> News { get; set; }

        public SkylineDbContext()
            : base("SkylineDb")
        {
            // Init database
            System.Data.Entity.Database.SetInitializer(new SkylineSeedData());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new NewsConfiguration());
            base.OnModelCreating(modelBuilder);
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
