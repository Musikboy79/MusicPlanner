using Dapper;
using MusicPlanner.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicPlanner.Controllers
{
    public class MP3FileUploadDownloadController : Controller
    {
        // GET: MP3FileUpload
        public ActionResult MP3FileUpload()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult MP3FileUpload(HttpPostedFileBase mp3Files)
        {
            String FileExt = Path.GetExtension(mp3Files.FileName).ToUpper();

            if (FileExt == ".MP3")
            {
                Stream str = mp3Files.InputStream;
                BinaryReader Br = new BinaryReader(str);
                Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                MP3FileDetailsModel Fd = new Models.MP3FileDetailsModel();
                Fd.FileName = mp3Files.FileName;
                Fd.FileContent = FileDet;
                SaveMP3FileDetails(Fd);
                return RedirectToAction("MP3FileUpload");
            }
            else
            {
                ViewBag.FileStatus = "Invalid File Format";
                return View();
            }
        }

        [Authorize]
        [HttpGet]
        public FileResult DownloadMP3(int id)
        {
            List<MP3FileDetailsModel> ObjFiles = GetMp3FileList();

            var FileById = (from FC in ObjFiles
                            where FC.Id.Equals(id)
                            select new { FC.FileName, FC.FileContent }).ToList().FirstOrDefault();
            return File(FileById.FileContent, "application/mp3", FileById.FileName);
        }

        [Authorize]
        [HttpGet]
        public PartialViewResult MP3FileDetails()
        {
            List<MP3FileDetailsModel> DetList = GetMp3FileList();

            return PartialView("MP3FileDetails", DetList);
        }

        [Authorize]
        private List<MP3FileDetailsModel> GetMp3FileList()
        {
            List<MP3FileDetailsModel> MP3List = new List<MP3FileDetailsModel>();
            DbConnection();
            con.Open();
            MP3List = SqlMapper.Query<MP3FileDetailsModel>
                (con, "GetMP3FileDetails", commandType: CommandType.StoredProcedure).ToList();
            con.Close();
            return MP3List;
        }

        [Authorize]
        private void SaveMP3FileDetails(MP3FileDetailsModel objMp3)
        {
            DynamicParameters Parm = new DynamicParameters();
            Parm.Add("@FileName", objMp3.FileName);
            Parm.Add("@FileContent", objMp3.FileContent);
            DbConnection();
            con.Open();
            con.Execute("AddMP3FileDetails", Parm, commandType: CommandType.StoredProcedure);
            con.Close();
        }

        private SqlConnection con;
        private string constr;
        private void DbConnection()
        {
            constr = ConfigurationManager.ConnectionStrings["dbcon"].ToString();
            con = new SqlConnection(constr);
        }
    }
}