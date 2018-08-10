using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BandDrop.Models
{
    public class Musician
    {
        public Musician()
        {
        }

        public int id { get; set; }
        public string name { get; set; }
        public DateTime created_at { get; set; }
    }
}