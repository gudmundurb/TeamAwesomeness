using Sozial.Models;
using SozialProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Sozial.Repositories
{
    public class GameRepo : IGameRepo, IDisposable
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /*
        public GameRepo(ApplicationDbContext db)
        {
            this.db = db;
        }
        public GameRepo() { }
         * */
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

        public void UpdateGame(SozialProject.Models.GameModel game)
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