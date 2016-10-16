using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skyline.Domain;

namespace Skyline.Services.Abstract
{
    public interface INewsService
    {
        IEnumerable<News> GetNews();
        News GetNews(int id);
        void CreateNews(News news);
        void SaveNews();
    }
}
