using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MusicPlanner.Models;

namespace MusicPlanner.Controllers
{
    public class ChoirController : Controller
    {
        private MusicMasterEntities db = new MusicMasterEntities();

        [Authorize]
        // GET: Choir
        public ActionResult Index()
        {
            var model = from r in db.Choir
                        orderby r.LastName
                        where r.Active == true
                        select r;
            return View(model.ToList());
        }

        // GET: Choir Unfiltered
        public ActionResult UnIndex()
        {
            return View(db.Choir.ToList());
        }

        [Authorize]
        // GET: Choir/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choir choir = db.Choir.Find(id);
            if (choir == null)
            {
                return HttpNotFound();
            }
            return View(choir);
        }

        [Authorize]
        // GET: Choir/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Choir/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChoirID,FirstName,LastName,Address1,City,State,Zip,HomePhone,CellPhone,Email,Active")] Choir choir)
        {
            if (ModelState.IsValid)
            {
                db.Choir.Add(choir);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(choir);
        }

        [Authorize]
        // GET: Choir/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choir choir = db.Choir.Find(id);
            if (choir == null)
            {
                return HttpNotFound();
            }
            return View(choir);
        }

        [Authorize]
        // POST: Choir/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChoirID,FirstName,LastName,Address1,City,State,Zip,HomePhone,CellPhone,Email,Active")] Choir choir)
        {
            if (ModelState.IsValid)
            {
                db.Entry(choir).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(choir);
        }

        [Authorize]
        // GET: Choir/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choir choir = db.Choir.Find(id);
            if (choir == null)
            {
                return HttpNotFound();
            }
            return View(choir);
        }

        [Authorize]
        // POST: Choir/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Choir choir = db.Choir.Find(id);
            db.Choir.Remove(choir);
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
