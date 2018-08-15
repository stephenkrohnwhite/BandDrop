using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BandDrop.Models;
using Microsoft.AspNet.Identity;

namespace BandDrop.Controllers
{
    public class MusiciansController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Musicians
        public ActionResult Index()
        {
            if(User.IsInRole("Musician"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var musicians = db.Musicians.Include(m => m.Band);
            return View(musicians.ToList());
        }

        // GET: Musicians/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Musician musician = db.Musicians.Find(id);
            if (musician == null)
            {
                return HttpNotFound();
            }
            return View(musician);
        }

        // GET: Musicians/Create
        public ActionResult Create()
        {
            ViewBag.BandId = new SelectList(db.Bands, "Id", "BandName");
            return View();
        }

        // POST: Musicians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name")] Musician musician)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                musician.UserId = userId;
                db.Musicians.Add(musician);
                db.SaveChanges();
                return RedirectToAction("Index", "Chat", "Chat");
            }

            ViewBag.BandId = new SelectList(db.Bands, "Id", "BandName", musician.BandId);
            return View(musician);
        }

        // GET: Musicians/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Musician musician = db.Musicians.Find(id);
            if (musician == null)
            {
                return HttpNotFound();
            }
            ViewBag.BandId = new SelectList(db.Bands, "Id", "BandName", musician.BandId);
            return View(musician);
        }

        // POST: Musicians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,created_at,BandId,BandName")] Musician musician)
        {
            if (ModelState.IsValid)
            {
                db.Entry(musician).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BandId = new SelectList(db.Bands, "Id", "BandName", musician.BandId);
            return View(musician);
        }

        // GET: Musicians/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Musician musician = db.Musicians.Find(id);
            if (musician == null)
            {
                return HttpNotFound();
            }
            return View(musician);
        }

        // POST: Musicians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Musician musician = db.Musicians.Find(id);
            db.Musicians.Remove(musician);
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
