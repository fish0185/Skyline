using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyline.Data.Configuration
{
    using System.Data.Entity.ModelConfiguration;

    using Skyline.Data.Entities;
    public class NewsConfiguration : EntityTypeConfiguration<News>
    {
        public NewsConfiguration()
        {
            ToTable("News");
            Property(news => news.NewsTitle).IsRequired().HasMaxLength(256);
        }
    }
}
