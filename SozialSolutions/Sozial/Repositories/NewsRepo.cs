using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Sozial.Models;

namespace Sozial.Repositories
{
    public class NewsRepo : INewsRepo, IDisposable
    {
        private ApplicationDbContext db = null; //new ApplicationDbContext();

        public NewsRepo(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<NewsModel> GetNews()
        {
            return db.NewsModels.ToList();
        }

        public IEnumerable<NewsModel> getRecentNews(int n)
        {
            return (from NewsModel news in db.NewsModels
                    orderby news.createdDate descending
                    select news).Take(n).ToList();
        }

        public NewsModel GetNewsByID(int? newsID)
        {
            return db.NewsModels.Find(newsID);
        }

        public void InsertNews(NewsModel news)
        {
            db.NewsModels.Add(news);
        }

        public void DeleteNews(int newsID)
        {
            NewsModel news = db.NewsModels.Find(newsID);
            db.NewsModels.Remove(news);
        }

        public void UpdateNews(NewsModel news)
        {
            db.Entry(news).State = EntityState.Modified;
        }

        public void SaveNews()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}