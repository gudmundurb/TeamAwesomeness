using Sozial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Sozial.Repositories
{
    public interface IGameRepo : IDisposable
    {
        IEnumerable<GameModel> GetGame();
        GameModel GetGameByID(int? gameID);
        void InsertGame(GameModel game);
        void DeleteGame(int gameID);
        void UpdateGame(GameModel game);
    }

    public interface IReviewRepo : IDisposable
    {
        IEnumerable<ReviewModel> GetReview();
        ReviewModel GetReviewById(int? reviewId);
        void InsertReview(ReviewModel review);
        void DeleteReview(int reviewId);
        void UpdateReview(ReviewModel review);
    }
}