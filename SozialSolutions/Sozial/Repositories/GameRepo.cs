using Sozial.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Sozial.Repositories
{
    public class GameRepo : IGameRepo, IDisposable
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
            /* add code here to get reviews. */ 
        }

        public void InsertGame(GameModel game)
        {
            db.GameModels.Add(game);
            db.SaveChanges();
        }

        public void DeleteGame(int gameID)
        {
            GameModel game = db.GameModels.Find(gameID);
            db.GameModels.Remove(game);
            db.SaveChanges();

        }

        public void UpdateGame(Sozial.Models.GameModel game)
        {
            db.Entry(game).State = EntityState.Modified;
            db.SaveChanges();

        }


        /* FAVEGAME */
        public IEnumerable<GameModel> getFaveGamesForUser(string username) {
            IEnumerable<int> gameIds = (from FavouriteRelationModel faves in db.FavouriteRelationModel
                                        where faves.username == username
                                        select faves.gameId).ToList();
            List<GameModel> userFaves = new List<GameModel>();
            foreach (int i in gameIds)
            {
                userFaves.Add(GetGameByID(i));
            }
            return userFaves.ToList();
        }

        

        /* DISPOSE */
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