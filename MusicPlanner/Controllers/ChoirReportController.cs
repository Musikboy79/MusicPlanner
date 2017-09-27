using CrystalDecisions.CrystalReports.Engine;
using MusicPlanner.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicPlanner.Controllers
{
    public class ChoirReportController : Controller
    {
        private MusicMasterEntities context = new MusicMasterEntities();
        // GET: ChoirReport
        public ActionResult Index()
        {
            var ChoirMemberList = context.Choir.ToList();
            return View(ChoirMemberList);
        }

        public ActionResult ExportChoirMembers()
        {
            List<Choir> allChoirMembers = new List<Choir>();
            allChoirMembers = context.Choir.ToList();

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/CrystalReports"), "ReportChoirMembers.rpt"));

            rd.SetDataSource(allChoirMembers);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "ChoirMemberList.pdf");
        }
    }
}