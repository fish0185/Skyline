using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyline.Data.Infrastructure
{
    using Skyline.Data.Concrete;

    public interface IDbFactory : IDisposable
    {
        SkylineDbContext Init();
    }
}
