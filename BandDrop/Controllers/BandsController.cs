using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using BandDrop.Models;
using BandDrop.Utils;
using BandDrop.ViewModels;
using Microsoft.AspNet.Identity;

namespace BandDrop.Controllers
{
    public class BandsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bands
        public ActionResult Index()
        {
            return View(db.Bands.ToList());
        }

        // GET: Bands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Band band = db.Bands.Find(id);
            if (band == null)
            {
                return HttpNotFound();
            }
            return View(band);
        }
        public ActionResult Join()
        {
            var bandJoinModel = new BandJoin();
            return View(bandJoinModel);

        }
        [HttpPost]
        public ActionResult Join([Bind(Include = "BandName")] BandJoin joinModel)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                var user = db.Musicians.FirstOrDefault(m => m.UserId == userId);
                var band = db.Bands.Where(b => b.BandName == joinModel.BandName).First();
                user.Band = band;
                user.BandId = band.Id;
                user.BandName = band.BandName;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }
        // GET: Bands/Create
        public ActionResult Create()
        {
            var bandModel = new BandViewModel();
            return View(bandModel);
        }

        // POST: Bands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Email_2,Email_3,Email_4,Email_5,Email_6,")] BandViewModel band)
        {
            if (ModelState.IsValid)
            {
                Band newBand = new Band();
                newBand.BandName = band.Name;
                db.Bands.Add(newBand);
                db.SaveChanges();
                EmailAllMembers(band);
                string userId = User.Identity.GetUserId();
                var user = db.Musicians.FirstOrDefault(m => m.UserId == userId);
                user.BandName = band.Name;
                var userBand = db.Bands.Where(b => b.BandName == band.Name).First();
                user.BandId = userBand.Id;
                user.Band = userBand;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(band);
        }

        private void EmailAllMembers(BandViewModel band)
        {
            List<string> bandEmails = GetEmails(band);
            for(int i = 0; i<bandEmails.Count; i++)
            {
                string receipient = bandEmails[i];
                string subject = "Join BANDdrop group!";
                string body = "Join group by entering : '" + band.Name + "' at band join page!";
                APIUtility.SendSimpleMessage(receipient, subject, body);
            }
            

        }

        private List<string> GetEmails(BandViewModel band)
        {
            List<string> bandList = new List<string>();
            if( CheckForNullString(band.Email_2))
            {
                bandList.Add(band.Email_2);
            }
            if(CheckForNullString(band.Email_3))
            {
                bandList.Add(band.Email_3);
            }
            if (CheckForNullString(band.Email_4))
            {
                bandList.Add(band.Email_4);
            }
            if (CheckForNullString(band.Email_5))
            {
                bandList.Add(band.Email_5);
            }
            if (CheckForNullString(band.Email_6))
            {
                bandList.Add(band.Email_6);
            }
            return bandList;
        }

        private bool CheckForNullString(string value)
        {
            if(value != null)
            {
                return true;
            }
            return false;
        }

        // GET: Bands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Band band = db.Bands.Find(id);
            if (band == null)
            {
                return HttpNotFound();
            }
            return View(band);
        }

        // POST: Bands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BandName")] Band band)
        {
            if (ModelState.IsValid)
            {
                db.Entry(band).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(band);
        }

        // GET: Bands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Band band = db.Bands.Find(id);
            if (band == null)
            {
                return HttpNotFound();
            }
            return View(band);
        }

        // POST: Bands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Band band = db.Bands.Find(id);
            db.Bands.Remove(band);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult SetBandImage(AudioFileVM fileupload)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            string currentUserId = User.Identity.GetUserId();
            var currentUser = db.Musicians.Where(m => m.UserId == currentUserId).First();
            var band = db.Bands.Where(b => b.Id == currentUser.BandId).First();
            if (fileupload.File != null && fileupload.File.ContentLength > 0)
            {
                var uploadDir = "~/Images";
                var imagePath = Path.Combine(Server.MapPath(uploadDir), fileupload.File.FileName);
                var imageUrl = Path.Combine(uploadDir, fileupload.File.FileName);
                fileupload.File.SaveAs(imagePath);
                band.BandImagePath = imageUrl;
                db.SaveChanges();

            }

            return RedirectToAction("Index", "Chat");
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
