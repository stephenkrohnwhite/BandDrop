using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BandDrop.ViewModels
{
    public class AudioFileVM
    {
        public string Name { get; set; }
        public HttpPostedFileBase File  {get;set;}
    }
}