using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security.Provider;
using MusicPlanner.Models;

namespace MusicPlanner.Controllers
{
    public class MonthlyMusicSheetController : Controller
    {
        // GET: MonthlyMusicSheet
        public ActionResult MonthlyMusicSheetUpload()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult MusicSheetUpload(HttpPostedFileBase files)
        {
            String FileExt = Path.GetExtension(files.FileName).ToUpper();

            if (FileExt == ".PDF")
            {
                Stream str = files.InputStream;
                BinaryReader Br = new BinaryReader(str);
                Byte[] FileDet = Br.ReadBytes((Int32) str.Length);

                MonthlyMusicSheetDetailsModel Md = new Models.MonthlyMusicSheetDetailsModel();
                Md.FileName = files.FileName;
                Md.FileContent = FileDet;
                SaveFileDetails(Md);
                return RedirectToAction("MusicSheetUpload");
            }

            if (FileExt == ".DOC")
            {
                Stream str = files.InputStream;
                BinaryReader Br = new BinaryReader(str);
                Byte[] FileDet = Br.ReadBytes((Int32) str.Length);

                MonthlyMusicSheetDetailsModel Md = new Models.MonthlyMusicSheetDetailsModel();
                Md.FileName = files.FileName;
                Md.FileContent = FileDet;
                SaveFileDetails(Md);
                return RedirectToAction("MusicSheetUpload");

            }
            else
            {
                ViewBag.FileStatus = "Invalid File Format";
                return View();
            }
        }
    }
}