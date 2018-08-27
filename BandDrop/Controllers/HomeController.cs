using BandDrop.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BandDrop.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            var musician = db.Musicians.Where(m => m.UserId == userId).SingleOrDefault();
            if (userId != null && musician.BandId != null)
            {
                return RedirectToAction("Index","Chat");
            }
            if(userId != null && musician.BandId == null)
            {
                return View("CreateOrJoin");
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}