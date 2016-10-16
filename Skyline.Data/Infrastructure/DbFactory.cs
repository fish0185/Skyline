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
        private SkylineDbContext dbContext;

        public SkylineDbContext Init()
        {
            return this.dbContext ?? (this.dbContext = new SkylineDbContext());
        }

        protected override void DisposeCore()
        {
            if (this.dbContext != null)
            {
                this.dbContext.Dispose();
            }
        }
    }
}
