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

        private SkylineDbContext _dbContext;

        public UnitOfWork(SkylineDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }
    }
}
