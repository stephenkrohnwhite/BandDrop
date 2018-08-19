using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BandDrop.Models
{
    public class AudioFile
    {
        [Key]
        public int ID { get; set; }
        [Display(Name ="Track Title")]
        public string Name { get; set; }
        public string FilePath { get; set; }
        public int BandId { get; set; }
        [Display(Name="Artist")]
        public string BandName { get; set; }
    }
}