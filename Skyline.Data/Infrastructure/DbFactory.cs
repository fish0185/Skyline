using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skyline.Data.Concrete;

namespace Skyline.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private SkylineDbContext _dbContext;

        public DbFactory(SkylineDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public SkylineDbContext Init()
        {
            return this._dbContext ?? (this._dbContext = new SkylineDbContext());
        }

        protected override void DisposeCore()
        {
            if (this._dbContext != null)
            {
                this._dbContext.Dispose();
            }
        }
    }
}
