using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BandDrop.Utils;
using PusherServer;

namespace BandDrop.Controllers
{
    public class MessageController : Controller
    {

        [HttpPost]
        public async Task<ActionResult> Create(string message)
        {

            var userName = User.Identity.Name;

            var options = new PusherOptions
            {
                Cluster = APIUtility.PusherCluster, //Repalce with your cluster 
                Encrypted = true
            };

            var pusher = new Pusher(
              APIUtility.PusherAppId, // Replace with your app_id
              APIUtility.PusherKey, //Replace with your key
              APIUtility.PusherSecretKey, //Replace with your secret 
              options);

            var result = await pusher.TriggerAsync(
              "my-channel", //Replace with your chanel name
              "my-event",    //Replace with your event name
              new { message = message, userName = userName });

            return new HttpStatusCodeResult((int)HttpStatusCode.OK);
        }

    }
}