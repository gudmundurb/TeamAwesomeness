using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sozial.Repositories
{
    public class PostRepo : IPostRepo, IDisposable
    {

        private ApplicationDbContext db = null; //new ApplicationDbContext();

        public GameRepo(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<GameModel> GetGame()
        {
            return db.GameModels.ToList();
        }

        public GameModel GetGameByID(int? gameID)
        {
            return db.GameModels.Find(gameID);
        }

        public void InsertGame(GameModel game)
        {
            db.GameModels.Add(game);
        }

        public void DeleteGame(int gameID)
        {
            GameModel game = db.GameModels.Find(gameID);
            db.GameModels.Remove(game);
        }

        public void UpdateGame(Sozial.Models.GameModel game)
        {
            db.Entry(game).State = EntityState.Modified;
        }

        public void SaveGame()
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