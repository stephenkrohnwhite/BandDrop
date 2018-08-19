using BandDrop.Models;
using BandDrop.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BandDrop.Controllers
{
    public class AudioFilesController : Controller
    {
        // GET: AudioFiles
        public ActionResult UploadAudio()
        {
            AudioFileVM track = new AudioFileVM();
            return View(track);
        }

        [HttpPost]
        public ActionResult UploadAudio(AudioFileVM fileupload)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            AudioFile track = new AudioFile();
            if (fileupload.File != null && fileupload.File.ContentLength > 0)
            {
                var uploadDir = "~/AudioFileUpload";
                var imagePath = Path.Combine(Server.MapPath(uploadDir), fileupload.File.FileName);
                var imageUrl = Path.Combine(uploadDir,fileupload.File.FileName);
                fileupload.File.SaveAs(imagePath);
                track.FilePath = imageUrl;
                track.Name = fileupload.Name;
                string userId = User.Identity.GetUserId();
                var user = db.Musicians.Where(m => m.UserId == userId).First();
                track.BandId = user.BandId.GetValueOrDefault();
                track.BandName = user.BandName;
                db.AudioFiles.Add(track);
                db.SaveChanges();

            }
            
            return RedirectToAction("Index", "Chat");
        }
    }
}