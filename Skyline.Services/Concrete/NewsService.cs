namespace Skyline.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Skyline.Data.Infrastructure;
    using Skyline.Domain;
    using Skyline.Services.Abstract;

    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;
        public NewsService(INewsRepository newsRepository)
        {
            this._newsRepository = newsRepository;
        }

        public void CreateNews(News news)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<News> GetNews()
        {
            return this._newsRepository.GetAll().Select(ToNewsDto);
        }

        private News ToNewsDto(Skyline.Data.Entities.News news)
        {
            return new News
                       {
                           Title = news.NewsTitle
                       };
        }

        public News GetNews(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveNews()
        {
            throw new NotImplementedException();
        }
    }
}
