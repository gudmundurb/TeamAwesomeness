using System;
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
    public class GameController : Controller
    {
        private IGameRepo db =  null; //new IGameRepo();
        
        //private ApplicationDbContext db = new ApplicationDbContext();
        //private IGameRepo db;
        
        public GameController()
        {
            this.db = new GameRepo(new ApplicationDbContext());
        }
       
        // GET: Game
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("Account/Login");
            }
            return View(db.GetGame());
        }

        // GET: Game/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //GameModel gameModel = db.GameModels.Find(id);
            GameModel gameModel = db.GetGameByID(id);
            if (gameModel == null)
            {
                return HttpNotFound();
            }
            return View(gameModel);
        }


        public ActionResult addGameToFave(int id)
        {
            RelationshipRepo relrepo = new RelationshipRepo(new ApplicationDbContext());
            relrepo.addGameToFavourites(id);
            return RedirectToAction("Index");
        }


        public ActionResult removeGameFromFave(int id)
        {
            RelationshipRepo relRepo = new RelationshipRepo(new ApplicationDbContext());
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
                db.InsertGame(gameModel);
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
            GameModel gameModel = db.GetGameByID(id);
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
                db.UpdateGame(gameModel);
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
            GameModel gameModel = db.GetGameByID(id);
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
            db.DeleteGame(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }


    //-------------------------------------------------------------------------


    public class ReviewController : Controller
    {
        private IReviewRepo db = null; //new IGameRepo();

        //private ApplicationDbContext db = new ApplicationDbContext();
        //private IGameRepo db;

        public ReviewController()
        {
            this.db = new ReviewRepo(new ApplicationDbContext());
        }

        // GET: Review
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("Account/Login");
            }
            return View(db.GetReview());
        }

        // GET: Review/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //GameModel gameModel = db.GameModels.Find(id);
            ReviewModel reviewModel = db.GetReviewById(id);
            if (reviewModel == null)
            {
                return HttpNotFound();
            }
            return View(reviewModel);
        }


        public ActionResult addGameToFave(int id)
        {
            RelationshipRepo relrepo = new RelationshipRepo(new ApplicationDbContext());
            relrepo.addGameToFavourites(id);
            return RedirectToAction("Index");
        }


        public ActionResult removeGameFromFave(int id)
        {
            RelationshipRepo relRepo = new RelationshipRepo(new ApplicationDbContext());
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
        public ActionResult Create([Bind(Include = "text")] ReviewModel reviewModel)
        {
            if (ModelState.IsValid)
            {
                db.InsertReview(reviewModel);
                return RedirectToAction("Index");
            }

            return View(reviewModel);
        }

        // GET: Game/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReviewModel reviewModel = db.GetReviewById(id);
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
                db.UpdateReview(reviewModel);
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
            ReviewModel reviewModel = db.GetReviewById(id);
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
            db.DeleteReview(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }


}
