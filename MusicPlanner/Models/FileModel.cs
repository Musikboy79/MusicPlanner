using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicPlanner.Models
{
    public class FileModel
    {
        [Required]
        [DataType(DataType.Upload)]
        [Display(Name ="Select File")]
        public HttpPostedFileBase files { get; set; }
    }
}