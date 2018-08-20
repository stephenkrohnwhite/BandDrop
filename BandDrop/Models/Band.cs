using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BandDrop.Models
{
    public class Band
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Band Name")]
        public string BandName { get; set; }
        public string BandImagePath { get; set; }

    }
}