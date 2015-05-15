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

        public GameRepo()
        {
            this.db = new ApplicationDbContext();
        }

        public IEnumerable<GameModel> GetGame()
        {
            return db.GameModels.ToList();
        }

        public GameModel GetGameByID(int? gameID)
        {
            ReviewRepo rRepo = new ReviewRepo(db);

            GameModel game = db.GameModels.Find(gameID);
            game.gameReview = rRepo.GetReviewsForGame(game.gameID).ToList();

            return game;
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

        public IEnumerable<GameModel> getHottestGames(int n)
        {
            IEnumerable<int> gameIDs = (from FavouriteRelationModel rel in db.FavouriteRelationModel
                                        select rel.gameId).ToList();

            List<GameModel> games = new List<GameModel>();

            if (gameIDs == null) { return null; }
            //enter the linq
            IEnumerable<int> q = from int s in gameIDs
                                 group s by s into g
                                 orderby g.Count() descending
                                 select g.Key;

            foreach (int i in q)
            {
                games.Add(GetGameByID(i));
            }
            return games.Take(n).ToList();
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




    //-----------------------------------------------------------------------


    public class ReviewRepo : IReviewRepo, IDisposable
    {
        private ApplicationDbContext db = null; //new ApplicationDbContext();

        public ReviewRepo(ApplicationDbContext db)
        {
            this.db = db;
        }

        public ReviewRepo()
        {
            this.db = new ApplicationDbContext();
        }


        public IEnumerable<ReviewModel> GetReviewsForGame(int gameId)
        {

            return (from Sozial.Models.ReviewModel model in db.ReviewModel
                    where model.gameId == gameId
                    select model).OrderByDescending(x => x.dateCreated).ToList();


        }

        
        

        public IEnumerable<ReviewModel> GetReview()
        {
            return db.ReviewModel.ToList();
        }

        public ReviewModel GetReviewById(int? gameID)
        {
            return db.ReviewModel.Find(gameID);
            // add code here to get reviews. 
        }
       
       public void InsertReview(ReviewModel review)
       {
           db.ReviewModel.Add(review);
           db.SaveChanges();
       }

       public void DeleteReview(int reviewId)
       {
           ReviewModel review = db.ReviewModel.Find(reviewId);
           db.ReviewModel.Remove(review);
           db.SaveChanges();
       }

       public void UpdateReview(ReviewModel review)
       {
           
           db.Entry(review).State = EntityState.Modified;
           db.SaveChanges();

       }


        
        // DISPOSE 
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