using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace BandDrop
{
    public class MyHub : Hub
    {
        public MyHub()
        {
            var taskTimer = Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    string timeNow = DateTime.Now.ToString();
                    Clients.All.SendServerTime(timeNow);
                    await Task.Delay(3000);
                }
                }, TaskCreationOptions.LongRunning
                );
        }
        public void Hello()
        {
            Clients.All.hello();
        }
        public void HelloServer()
        {
            Clients.All.hello("Hello message to all clients");
        }

    }
}