using BandDrop.Models;
using Microsoft.AspNet.Identity;
using PusherServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BandDrop.Utils;

namespace BandDrop.Controllers
{
    public class AuthController : Controller
    {
        [HttpPost]
        public ActionResult Login()
        {
            string user_name = Request.Form["username"];

            if (user_name.Trim() == "")
            {
                return Redirect("/");
            }

            using (var db = new Models.ApplicationDbContext())
            {

                Musician user = db.Musicians.FirstOrDefault(m => m.name == user_name);

                if (user == null)
                {
                    user = new Musician{ name = user_name };

                    db.Musicians.Add(user);
                    db.SaveChanges();
                }

                Session["user"] = user;
            }

            return Redirect("/chat");
        }
        public JsonResult AuthForChannel(string channel_name, string socket_id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            string userId = User.Identity.GetUserId();
            var currentUser = db.Musicians.Where(u => u.UserId == userId).First();
            if (currentUser == null)
            {
                return Json(new { status = "error", message = "User is not logged in" });
            }


            var options = new PusherOptions();
            options.Cluster = APIUtility.PusherCluster;

            var pusher = new Pusher(
            APIUtility.PusherAppId,
            APIUtility.PusherKey,
            APIUtility.PusherSecretKey, options);
            
            //if (channel_name.IndexOf(currentUser.id.ToString()) == -1)
            //{
            //    return Json(
            //      new { status = "error", message = "User cannot join channel" }
            //    );
            //}

            var auth = pusher.Authenticate(channel_name, socket_id);

            return Json(auth);
        }
    }
}