using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicPlanner.Models
{
    public class MonthlyMusicSheetDetailsModel
    {
        public int Id { get; set; }
        [Display(Name = "Available Monthly Music Sheets")]
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }

    }
}