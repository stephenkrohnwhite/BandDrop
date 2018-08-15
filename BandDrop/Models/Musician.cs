using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BandDrop.Models
{
    public class Musician
    {

        [Key]
        public int id { get; set; }
        public string UserId { get; set; }
        [Display(Name ="Musician Name")]
        public string name { get; set; }
        [ForeignKey("Band")]
        public int? BandId { get; set; }
        public Band Band { get; set; }
        [Display(Name = "Band Name")]
        public string BandName { get; set; }
 
    }
}