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
                if (Session["user"] == null)
                {
                    return Redirect("/");
                }

                var currentUser = (Models.Musician)Session["user"];

                using (var db = new Models.ApplicationDbContext())
                {

                    ViewBag.allUsers = db.Musicians.Where(m => m.name != currentUser.name)
                                     .ToList();
                }


                ViewBag.currentUser = currentUser;


                return View();
            }
        
    }
}