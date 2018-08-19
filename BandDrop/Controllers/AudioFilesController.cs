using BandDrop.Models;
using BandDrop.ViewModels;
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
        public ActionResult UploadAudio(HttpPostedFileBase fileupload)
        {
            if (fileupload != null)
            {
                string fileName = Path.GetFileName(fileupload.FileName);
                int fileSize = fileupload.ContentLength;
                int Size = fileSize / 1000000;
                fileupload.SaveAs(Server.MapPath("~/AudioFileUpload/" + fileName));

                string CS = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("spAddNewAudioFile", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Name", fileName);
                    cmd.Parameters.AddWithValue("FilePath", "~/AudioFileUpload/" + fileName);
                    cmd.ExecuteNonQuery();
                }
            }
            return RedirectToAction("UploadAudio");
        }
    }
}