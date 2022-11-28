using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using Travel.Convertor;
using Travel.Models;
using Travel.TravelDbContext;
using Travel.ViewModel;

namespace Travel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TravelContext _context;

        public HomeController(ILogger<HomeController> logger, TravelContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ExportFireP04TafkikListToExcel(ReportRequest data)
        {


            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[47] {
                                                    new DataColumn("PersonName"),
                                                    new DataColumn("PersonLname"),
                                                    new DataColumn("PersonJens"),
                                                    new DataColumn("PersonKind"),
                                                    new DataColumn("CodeMelli"),
                                                    new DataColumn("CompanyCode"),
                                                    new DataColumn("IdentityNo"),
                                                    new DataColumn("BirthDay"),
                                                    new DataColumn("BirthMonth"),
                                                    new DataColumn("BirthYear"),
                                                    new DataColumn("Sodurplace"),
                                                    new DataColumn("Mobile"),
                                                    new DataColumn("Tel"),
                                                    new DataColumn("PersonAddress"),
                                                    new DataColumn("CodePosti"),
                                                    new DataColumn("LatinName"),
                                                    new DataColumn("LatinlName"),
                                                    new DataColumn("IsIranian"),
                                                    new DataColumn("FatherName"),
                                                    new DataColumn("Gid"),
                                                    new DataColumn("UnIranianCode"),
                                                    new DataColumn("City"),
                                                    new DataColumn("FishNo"),
                                                    new DataColumn("FishDate"),
                                                    new DataColumn("Bank"),
                                                    new DataColumn("Seri"),
                                                    new DataColumn("Serial"),
                                                    new DataColumn("MbeginDate"),
                                                    new DataColumn("PassportNo"),
                                                    new DataColumn("MsodurDate"),
                                                    new DataColumn("GrpTakhfif"),
                                                    new DataColumn("CoverLimit"),
                                                    new DataColumn("SafarKind"),
                                                    new DataColumn("YearBirth"),
                                                    new DataColumn("DayBirth"),
                                                    new DataColumn("MonthBirth"),
                                                    new DataColumn("ModdatKind"),
                                                    new DataColumn("Moddat"),
                                                    new DataColumn("Country"),
                                                    new DataColumn("LocationZoneId"),
                                                    new DataColumn("ArzType"),
                                                    new DataColumn("MoenyUnitRateId"),
                                                    new DataColumn("MbirthYear"),
                                                    new DataColumn("CodeSodur"),
                                                    new DataColumn("AgentCode"),
                                                    new DataColumn("Bimekind"),
                                                    new DataColumn("IsInFreeRegion")
                                                  });
           
            var lst = from ply in _context.TravelBn
                      where ply.IssueDateMiladi>= data.FromDate && ply.IssueDateMiladi<=data.ToDate  
                      && ply.IsSendToFan==false
                      select ply;
            foreach (var item in lst)
            {
                dt.Rows.Add(item.PersonName, item.PersonLname, item.PersonJens, item.PersonKind, item.CodeMelli,
                             item.CompanyCode, item.IdentityNo, item.BirthDay, item.BirthMonth, item.BirthYear,
                             item.Sodurplace, item.Mobile, item.Tel, item.PersonAddress, item.CodePosti, item.LatinName,
                             item.LatinlName, item.IsIranian , item.FatherName, item.Gid, item.UnIranianCode, item.City,
                             item.FishNo, item.FishDate, item.Bank, item.Seri, item.Serial, item.MbeginDate,
                             item.PassportNo, item.MsodurDate, item.GrpTakhfif, item.CoverLimit, item.SafarKind, item.YearBirth,
                             item.DayBirth, item.MonthBirth, item.ModdatKind, item.Moddat, item.Country, item.LocationZoneId,
                             item.ArzType, item.MoenyUnitRateId, item.MbirthYear, item.CodeSodur, item.AgentCode, item.Bimekind,item.IsInFreeRegion
                             );

            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream st = new MemoryStream())
                {
                    wb.SaveAs(st);
                    return File(st.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheatml.sheet", "Grid.xlsx");
                }
            }
        }





    }
}