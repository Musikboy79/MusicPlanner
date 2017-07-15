using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicPlanner.Models
{
    public class EmpModel
    {
        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Select File")]
        public HttpPostedFileBase files { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Select MP3 File")]
        public HttpPostedFileBase mp3Files { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Select Music Schedule File")]
        public HttpPostedFileBase schedMusicSheetFiles { get; set; }
    }

}