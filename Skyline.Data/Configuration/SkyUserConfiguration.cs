using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyline.Data.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using Skyline.Data.Entities;

    public class SkyUserConfiguration : EntityTypeConfiguration<SkylineUser>
    {
        public SkyUserConfiguration()
        {
            Property(user => user.DOB).HasColumnType("datetime2").IsOptional();
            
        }
    }
}
