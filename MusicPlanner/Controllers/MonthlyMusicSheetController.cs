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
using System.Data.Common;

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
        public ActionResult MonthlyMusicSheetUpload(HttpPostedFileBase files)
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
                return RedirectToAction("MonthlyMusicSheetUpload");
            }
            else
            {
                ViewBag.FileStatus = "Invalid File Format";
                return View();
            }
        }

        [Authorize]
        [HttpGet]
        public FileResult DownloadFile(int id)
        {
            List<MonthlyMusicSheetDetailsModel> ObjFiles = GetFileList();

            var FileById = (from FC in ObjFiles
                    where FC.Id.Equals(id)
                select new {FC.FileName, FC.FileContent}).ToList().FirstOrDefault();
            return File(FileById.FileContent, "application/pdf", FileById.FileName);
        }

        [Authorize]
        [HttpGet]
        public PartialViewResult MusicSheetFileDetails()
        {
            List<MonthlyMusicSheetDetailsModel> DetList = GetFileList();

            return PartialView("MonthlyMusicSheetDetails", DetList);
        }

        [Authorize]
        private List<MonthlyMusicSheetDetailsModel> GetFileList()
        {
            List<MonthlyMusicSheetDetailsModel> DetList = new List<MonthlyMusicSheetDetailsModel>();
            DbConnection();
            con.Open();
            DetList = SqlMapper.Query<MonthlyMusicSheetDetailsModel>
                (con, "GetMonthlyMusicSheetFileDetails", commandType:CommandType.StoredProcedure).ToList();
            con.Close();
            return DetList;
        }

        [Authorize]
        private void SaveFileDetails(MonthlyMusicSheetDetailsModel objDet)
        {
            DynamicParameters Parm = new DynamicParameters();
            Parm.Add("@FileName", objDet.FileName);
            Parm.Add("@FileContent", objDet.FileContent);
            DbConnection();
            con.Open();
            con.Execute("AddMonthlyMusicFileDetails", Parm, commandType: CommandType.StoredProcedure);
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