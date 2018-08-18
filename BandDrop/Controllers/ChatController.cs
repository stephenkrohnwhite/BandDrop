using BandDrop.Models;
using BandDrop.Utils;
using Microsoft.AspNet.Identity;
using PusherServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BandDrop.Controllers
{
    public class ChatController : Controller
    {
        private Pusher pusher;

        //class constructor
        public ChatController()
        {
            var options = new PusherOptions();
            options.Cluster = APIUtility.PusherCluster;

            pusher = new Pusher(
               APIUtility.PusherAppId,
               APIUtility.PusherKey,
               APIUtility.PusherSecretKey,
               options
           );
        }

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
        [HttpGet]
        public JsonResult Conversations(int id)
          {
            
            ApplicationDbContext db = new ApplicationDbContext();
            string userId = User.Identity.GetUserId();
            var currentUser = db.Musicians.Where(u => u.UserId == userId).First();
            if (currentUser == null)
              {
                  return Json(new { status = "error", message = "User is not logged in" });
              }
             var conversations = new List<Models.Conversation>();
             conversations = db.Conversations.
                                    Where(c => (c.receiver_id == currentUser.id
                                        && c.sender_id == id) ||
                                        (c.receiver_id == id
                                        && c.sender_id == currentUser.id))
                                      .OrderBy(c => c.created_at)
                                    .ToList();
              return Json(
                  new { status = "success", data = conversations },
                  JsonRequestBehavior.AllowGet
              );
          }
          [HttpPost]
          public JsonResult SendMessage()
          {
              ApplicationDbContext db = new ApplicationDbContext();
              string userId = User.Identity.GetUserId();
              var currentUser = db.Musicians.Where(u => u.UserId == userId).First();
              if(currentUser == null)
              {
                  return Json(new { status = "error", message = "User is not logged in" });
              }
            
              string socket_id = Request.Form["socket_id"];

            Conversation convo = new Conversation
            {
                sender_id = currentUser.id,
                sender_name = currentUser.name,
                message = Request.Form["message"],
                receiver_id = Convert.ToInt32(Request.Form["contact"]),
                created_at = DateTime.Now
              };
              db.Conversations.Add(convo);
              db.SaveChanges();


              var conversationChannel = getConvoChannel(currentUser.id, convo.receiver_id);

              pusher.TriggerAsync(
                conversationChannel,
                "new_message",
                convo,
                new TriggerOptions() { SocketId = socket_id });


            return Json(convo);
        }
        private String getConvoChannel(int user_id, int contact_id)
        {
            if (user_id > contact_id)
            {
                return "private-chat-" + contact_id + "-" + user_id;
            }

            return "private-chat-" + user_id + "-" + contact_id;
        }

    }

}