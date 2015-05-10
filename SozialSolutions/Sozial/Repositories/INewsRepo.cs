using Sozial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sozial.Repositories
{
    public interface INewsRepo : IDisposable
    {
        IEnumerable<NewsModel> GetNews();
        NewsModel GetNewsByID(int? newsID);
        void InsertNews(NewsModel news);
        void DeleteNews(int newsID);
        void UpdateNews(NewsModel post);
        void SaveNews(); // <- Should each function above not just call db.SaveChanges(); ?
    }
}