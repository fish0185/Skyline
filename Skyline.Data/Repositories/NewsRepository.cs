using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyline.Data.Repositories
{
    using Skyline.Data.Concrete;
    using Skyline.Data.Entities;
    using Skyline.Data.Infrastructure;
    public class NewsRepository : RepositoryBase<News>, INewsRepository
    {
        public NewsRepository(SkylineDbContext dbContext) : base(dbContext) { }
    }
}
