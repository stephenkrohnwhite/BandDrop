using BandDrop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}