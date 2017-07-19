using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using MusicPlanner.Models;

namespace MusicPlanner.Controllers
{
    public class SchedMusicUploadDownloadController : Controller
    {
        [Authorize]
        #region Upload File
        public ActionResult FileUpload()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase schedMusicSheetFiles)
        {
            String FileExt = Path.GetExtension(schedMusicSheetFiles.FileName).ToUpper();

            if (FileExt == ".PDF")
            {
                Stream str = schedMusicSheetFiles.InputStream;
                BinaryReader Br = new BinaryReader(str);
                Byte[] FileDet = Br.ReadBytes((Int32) str.Length);

                SchedMusicFileDetailsModel Sd = new SchedMusicFileDetailsModel();
                Sd.FileName = schedMusicSheetFiles.FileName;
                Sd.FileContent = FileDet;
                SaveFileDetails(Sd);
                return RedirectToAction("FileUpload");
            }
            else
            {
                ViewBag.FileStatus = "Invalid File Format";
                return View();
            }
        }

       #endregion

        [Authorize]
        [HttpGet]
        public FileResult DownloadMusicSheet(int id)
        {
            List<SchedMusicFileDetailsModel> ObjFiles = GetFileList();

            var FileById = (from FC in ObjFiles
                where FC.Id.Equals(id)
                select new {FC.FileName, FC.FileContent}).ToList().FirstOrDefault();
            return File(FileById.FileContent, "application/pdf", FileById.FileName);
        }

        [Authorize]
        [HttpGet]
        public PartialViewResult FileDetails()
        {
            List<SchedMusicFileDetailsModel> DetList = GetFileList();

            return PartialView("FileDetails", DetList);

        }

        [Authorize]
        private List<SchedMusicFileDetailsModel> GetFileList()
        {
            List<SchedMusicFileDetailsModel> DetList = new List<SchedMusicFileDetailsModel>();

            DbConnection();
            con.Open();
            DetList = SqlMapper
                .Query<SchedMusicFileDetailsModel>(con, "GetSchedMusicFileDetails", commandType: CommandType.StoredProcedure)
                .ToList();
            con.Close();
            return DetList;
        }

        [Authorize]
        public ActionResult DeleteMusicSheet(SchedMusicFileDetailsModel objDet)
        {
            DynamicParameters Parm = new DynamicParameters();
            Parm.Add("@ID", objDet.Id);
            DbConnection();
            con.Open();
            con.Execute("DeleteSchedMusicFileDetails", Parm, commandType: CommandType.StoredProcedure);
            con.Close();

            return RedirectToAction("FileUpload");

        }

        [Authorize]
        private void SaveFileDetails(SchedMusicFileDetailsModel objDet)
        {
            DynamicParameters Parm = new DynamicParameters();
            Parm.Add("@FileName", objDet.FileName);
            Parm.Add("@FileContent", objDet.FileContent);
            DbConnection();
            con.Open();
            con.Execute("AddSchedMusicFileDetails", Parm, commandType: CommandType.StoredProcedure);
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