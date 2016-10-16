using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyline.Data.Infrastructure
{
    using Skyline.Data.Concrete;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;

        private SkylineDbContext dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public SkylineDbContext DbContext
        {
            get { return this.dbContext??(this.dbContext = this.dbFactory.Init());}
        }

        public int Commit()
        {
            return DbContext.SaveChanges();
        }
    }
}
