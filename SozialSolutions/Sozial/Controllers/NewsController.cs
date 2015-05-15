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

    public class NewsController : Controller
    {
        private INewsRepo newsRepo = null; //new INewsRepo();

        public NewsController()
        {
            this.newsRepo = new NewsRepo();
        }

        // GET: News
        public ActionResult Index()
        {
            IEnumerable<NewsModel> newsList = newsRepo.GetNews();

            
            return View( newsList.OrderByDescending(x => x.createdDate) );
        }

        // GET: News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsModel newsModel = newsRepo.GetNewsByID(id);
            if (newsModel == null)
            {
                return HttpNotFound();
            }
            return View(newsModel);
        }
        // GET: News/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "newsID,userID,title,text,imageUrl")] NewsModel newsModel)
        {
            if (ModelState.IsValid)
            {
                newsModel.userID = User.Identity.Name;
                newsRepo.InsertNews(newsModel);
                newsRepo.SaveNews();
                return RedirectToAction("Index");
            }

            return View(newsModel);
        }

        // GET: News/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsModel newsModel = newsRepo.GetNewsByID(id);
            if (newsModel == null)
            {
                return HttpNotFound();
            }
            return View(newsModel);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "newsID,userID,title,text,imageUrl")] NewsModel newsModel)
        {
            if (ModelState.IsValid)
            {
                newsRepo.UpdateNews(newsModel);
                newsRepo.SaveNews();
                return RedirectToAction("Index");
            }
            return View(newsModel);
        }

        // GET: News/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsModel newsModel = newsRepo.GetNewsByID(id);
            if (newsModel == null)
            {
                return HttpNotFound();
            }
            return View(newsModel);
        }

        // POST: News/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewsModel newsModel = newsRepo.GetNewsByID(id);
            newsRepo.DeleteNews(id);
            newsRepo.SaveNews();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                newsRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
