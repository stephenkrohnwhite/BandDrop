using BandDrop.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BandDrop.Controllers
{
    public class ChatController : Controller
    {
          public ActionResult Index()
          {
              ApplicationDbContext db = new ApplicationDbContext();
              string userId = User.Identity.GetUserId();
              var currentUser = db.Musicians.Where(u => u.UserId == userId).First();

              ViewBag.allUsers = db.Musicians.Where(m => m.name != currentUser.name).Where(m => m.BandId == currentUser.BandId)
                                     .ToList();
             ViewBag.currentUser = currentUser;


              return View();
          }
        
    }
}