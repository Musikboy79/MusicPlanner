using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicPlanner.Models
{
    public class FileViewModel
    {
        public string Title { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}