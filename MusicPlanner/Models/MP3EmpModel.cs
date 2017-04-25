using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicPlanner.Models
{
    public class MP3EmpModel
    {
        [Required]
        [DataType(DataType.Upload)]
        [Display(Name ="Select File")]
        public HttpPostedFileBase mp3Files { get; set; }
    }
}