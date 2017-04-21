using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicPlanner.Models
{
    public class MP3FileDetailsModel
    {
        public int Id { get; set; }
        [Display(Name="Available Files")]
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }

    }
}