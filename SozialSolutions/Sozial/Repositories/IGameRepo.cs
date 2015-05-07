using SozialProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sozial.Repositories
{
    public interface IGameRepo : IDisposable
    {
        IEnumerable<GameModel> GetGame();
        GameModel GetGameByID(int gameID);
        void InsertGame(GameModel game);
        //void DeleteGame(int gameID);
        void UpdateGame(GameModel game);
        void Save();
    }
}