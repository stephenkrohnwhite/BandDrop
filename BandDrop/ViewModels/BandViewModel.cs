using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BandDrop.ViewModels
{
    public partial class BandViewModel
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        
        [Display(Name = "Bandmate email")]
        public string Email_2 { get; set; }
        [Display(Name = "Bandmate email")]
        public string Email_3 { get; set; }
        [Display(Name = "Bandmate email")]
        public string Email_4 { get; set; }
        [Display(Name = "Bandmate email")]
        public string Email_5 { get; set; }
        [Display(Name = "Bandmate email")]
        public string Email_6 { get; set; }
    }
}