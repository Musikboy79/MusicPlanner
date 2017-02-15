using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MusicPlanner;

namespace MusicPlanner.Controllers
{
    public class MonthlyMusicController : Controller
    {
        private MusicMasterEntities db = new MusicMasterEntities();

        // GET: MonthlyMusic
        public ActionResult Index()
        {
            return View(db.NewMonthlyMusic.ToList());
        }

        // GET: MonthlyMusic/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewMonthlyMusic newMonthlyMusic = db.NewMonthlyMusic.Find(id);
            if (newMonthlyMusic == null)
            {
                return HttpNotFound();
            }
            return View(newMonthlyMusic);
        }

        // GET: MonthlyMusic/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MonthlyMusic/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MusicID,Date,churchDay,Opening,OpeningNumber,Psalm,PsalmNumber,Preparation,PreparationNumber,Communion,CommunionNumber,Closing,ClosingNumber,Notes")] NewMonthlyMusic newMonthlyMusic)
        {
            if (ModelState.IsValid)
            {
                db.NewMonthlyMusic.Add(newMonthlyMusic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(newMonthlyMusic);
        }

        // GET: MonthlyMusic/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewMonthlyMusic newMonthlyMusic = db.NewMonthlyMusic.Find(id);
            if (newMonthlyMusic == null)
            {
                return HttpNotFound();
            }
            return View(newMonthlyMusic);
        }

        // POST: MonthlyMusic/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MusicID,Date,churchDay,Opening,OpeningNumber,Psalm,PsalmNumber,Preparation,PreparationNumber,Communion,CommunionNumber,Closing,ClosingNumber,Notes")] NewMonthlyMusic newMonthlyMusic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newMonthlyMusic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newMonthlyMusic);
        }

        // GET: MonthlyMusic/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewMonthlyMusic newMonthlyMusic = db.NewMonthlyMusic.Find(id);
            if (newMonthlyMusic == null)
            {
                return HttpNotFound();
            }
            return View(newMonthlyMusic);
        }

        // POST: MonthlyMusic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewMonthlyMusic newMonthlyMusic = db.NewMonthlyMusic.Find(id);
            db.NewMonthlyMusic.Remove(newMonthlyMusic);
            db.SaveChanges();
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
