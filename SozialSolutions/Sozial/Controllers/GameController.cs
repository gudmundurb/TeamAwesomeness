﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sozial.Models;
using Sozial.Repositories;

namespace Sozial.Controllers
{

    [Authorize]
    public class GameController : Controller
    {
        private IGameRepo gameRepo =  null; //new IGameRepo();
        private RelationshipRepo relRepo = new RelationshipRepo();
        //private ApplicationDbContext db = new ApplicationDbContext();
        //private IGameRepo db;
        
        public GameController()
        {
            this.gameRepo = new GameRepo();
        }
       
        // GET: Game
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("Account/Login");
            }
            return View(gameRepo.GetGame());
        }

        // GET: Game/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //GameModel gameModel = db.GameModels.Find(id);
            GameModel gameModel = gameRepo.GetGameByID(id);
            if (gameModel == null)
            {
                return HttpNotFound();
            }
            return View(gameModel);
        }


        public ActionResult addGameToFave(int id)
        {
            
            relRepo.addGameToFavourites(id);
            return RedirectToAction("Index");
        }


        public ActionResult removeGameFromFave(int id)
        {
            relRepo.removeFromFavourites(id);
            return RedirectToAction("../Home/Profile");
        }



        // GET: Game/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Game/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GameID,nameOfGame,aboutGame,gameCompany,isTopTen,genre, imageUrl")] GameModel gameModel)
        {
            if (ModelState.IsValid)
            {
                gameRepo.InsertGame(gameModel);
                return RedirectToAction("Index");
            }

            return View(gameModel);
        }

        // GET: Game/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameModel gameModel = gameRepo.GetGameByID(id);
            if (gameModel == null)
            {
                return HttpNotFound();
            }
            return View(gameModel);
        }

        // POST: Game/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GameID,nameOfGame,aboutGame,gameCompany,isTopTen,genre,imageUrl")] GameModel gameModel)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(gameModel).State = EntityState.Modified;
                gameRepo.UpdateGame(gameModel);
                return RedirectToAction("Index");
            }
            return View(gameModel);
        }

        // GET: Game/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameModel gameModel = gameRepo.GetGameByID(id);
            if (gameModel == null)
            {
                return HttpNotFound();
            }
            return View(gameModel);
        }

        // POST: Game/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            gameRepo.DeleteGame(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                gameRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }


    //-------------------------------------------------------------------------


    public class ReviewController : Controller
    {
        private IReviewRepo revRepo = null; //new IGameRepo();
        RelationshipRepo relRepo = new RelationshipRepo();
        //private ApplicationDbContext db = new ApplicationDbContext();
        //private IGameRepo db;

        public ReviewController()
        {
            this.revRepo = new ReviewRepo();
        }


        // GET: Review
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("Account/Login");
            }
            return View(revRepo.GetReview());
        }


        // GET: Review/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //GameModel gameModel = db.GameModels.Find(id);
            ReviewModel reviewModel = revRepo.GetReviewById(id);
            if (reviewModel == null)
            {
                return HttpNotFound();
            }
            return View(reviewModel);
        }


        public ActionResult addGameToFave(int id)
        {
            
            relRepo.addGameToFavourites(id);
            return RedirectToAction("Index");
        }


        public ActionResult removeGameFromFave(int id)
        {
            relRepo.removeFromFavourites(id);
            return RedirectToAction("../Home/Profile");
        }



        // GET: Review/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: Game/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReviewEnvelope sunshine)
        {
            ReviewModel reviewModel = sunshine.review;

            reviewModel.gameId = sunshine.gameId;
            reviewModel.dateCreated = DateTime.Now;
            reviewModel.userId = User.Identity.Name;
            revRepo.InsertReview(reviewModel);

            return RedirectToAction("../Game/Details/" + sunshine.gameId.ToString());
        }


        // GET: Game/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReviewModel reviewModel = revRepo.GetReviewById(id);
            if (reviewModel == null)
            {
                return HttpNotFound();
            }
            return View(reviewModel);
        }


        // POST: Game/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "text")] ReviewModel reviewModel)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(gameModel).State = EntityState.Modified;
                revRepo.UpdateReview(reviewModel);
                return RedirectToAction("Index");
            }
            return View(reviewModel);
        }


        // GET: Game/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReviewModel reviewModel = revRepo.GetReviewById(id);
            if (reviewModel == null)
            {
                return HttpNotFound();
            }
            return View(reviewModel);
        }

        // POST: Game/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            revRepo.DeleteReview(id);
            return RedirectToAction("Index");
        }
    }
}
