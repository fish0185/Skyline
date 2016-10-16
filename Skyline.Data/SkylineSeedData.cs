using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyline.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;

    using Skyline.Data.Concrete;
    using Skyline.Data.Entities;

    public class SkylineSeedData : DropCreateDatabaseIfModelChanges<SkylineDbContext>
    {
        protected override void Seed(SkylineDbContext context)
        {
            GetNews().ForEach(news=>context.News.Add(news));
            context.SaveChanges();
        }

        private static List<News> GetNews()
        {
            return new List<News>
                       {
                           new News
                               {
                                   NewsTitle = "Unit 807 For Sale"
                               },
                           new News
                               {
                                   NewsTitle = "Fire alarm check"
                               },
                           new News
                               {
                                   NewsTitle = "Lift Broken"
                               }
                       };
        }
    }
}
