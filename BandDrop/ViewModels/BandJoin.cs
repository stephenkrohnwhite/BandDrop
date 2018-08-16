using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BandDrop.ViewModels
{
    public class BandJoin
    {
        [Key]
        public int id { get; set; }
        [Display(Name ="Band Name")]
        public string BandName { get; set; }
    }
}