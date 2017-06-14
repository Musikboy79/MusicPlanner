using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicPlanner.Models
{
    public class MonthlyMusicSheetModel
    {
        [Required]
        [DataType(DataType.Upload)]
        [Display(Name="Select File")]
        public HttpPostedFileBase musicSheetFiles { get; set; }
    }
}