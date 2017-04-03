using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicPlanner.Models;
using System.IO;
using Dapper;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace MusicPlanner.Controllers
{
    public class FileUploadDownloadController : Controller
    {
        [Authorize]
        // GET: FileUploadDownload
        public ActionResult FileUpload()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase files)
        {
            String FileExt = Path.GetExtension(files.FileName).ToUpper();

            if (FileExt ==".PDF")
            {
                Stream str = files.InputStream;
                BinaryReader Br = new BinaryReader(str);
                Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                FileDetailsModel Fd = new Models.FileDetailsModel();
                Fd.FileName = files.FileName;
                Fd.FileContent = FileDet;
                SaveFileDetails(Fd);
                return RedirectToAction("FileUpload");
            }
            if (FileExt == ".MP3")
            {
                Stream str = files.InputStream;
                BinaryReader Br = new BinaryReader(str);
                Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                FileDetailsModel Fd = new Models.FileDetailsModel();
                Fd.FileName = files.FileName;
                Fd.FileContent = FileDet;
                SaveFileDetails(Fd);
                return RedirectToAction("FileUpload");
            }
            else
            {
                ViewBag.FileStatus = "Invalid File Format";
                return View();
            }
        }

        [Authorize]
        [HttpGet]
        public FileResult DownloadFile (int id)
        {
            List<FileDetailsModel> ObjFiles = GetFileList();

            var FileById = (from FC in ObjFiles
                            where FC.Id.Equals(id)
                            select new { FC.FileName, FC.FileContent }).ToList().FirstOrDefault();
            return File(FileById.FileContent, "application/pdf", FileById.FileName);
        }

        [Authorize]
        [HttpGet]
        public PartialViewResult FileDetails()
        {
            List<FileDetailsModel> DetList = GetFileList();

            return PartialView("FileDetails", DetList);
        }

        [Authorize]
        private List<FileDetailsModel> GetFileList()
        {
            List<FileDetailsModel> DetList = new List<FileDetailsModel>();
            DbConnection();
            con.Open();
            DetList = SqlMapper.Query<FileDetailsModel>
                (con, "GetFileDetails", commandType: CommandType.StoredProcedure).ToList();
            con.Close();
            return DetList;
        }

        [Authorize]
        private void SaveFileDetails(FileDetailsModel objDet)
        {
            DynamicParameters Parm = new DynamicParameters();
            Parm.Add("@FileName", objDet.FileName);
            Parm.Add("@FileContent", objDet.FileContent);
            DbConnection();
            con.Open();
            con.Execute("AddFileDetails", Parm, commandType: System.Data.CommandType.StoredProcedure);
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