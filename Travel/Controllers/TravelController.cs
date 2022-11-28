using AutoMapper;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.ServiceModel;
using Travel.Convertor;
using Travel.Models.Travel;
using Travel.Security;
using Travel.Services.TravelServices;
using Travel.TravelDbContext;
using Travel.ViewModel;
using TravelService;

namespace Travel.Controllers
{
    public class TravelController : Controller
    {
        private readonly TravelContext _context;
        private readonly IMapper _mapper;
       

      
        public TravelController(TravelContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public IActionResult Index()
        {
            return View("AllCreatedPolicy");
        }



        [PermissionChecker(15)]
        public IActionResult LoadCreateOrEdit(int id)
        {


            TravelVM bnk = new();
            bnk.Invoice = new();
            bnk.BimeGozar = new();
            bnk.BimeGozar.PersonJensName = _context.VPersonJens.Select(c => new ViewModel.PersonJens { PersonJensId = c.PersonJensId, PersonJensName = c.PersonJensName }).ToList();
            bnk.BimeGozar.PersonKindName = _context.VPersonKind.Select(c => new ViewModel.PersonKind { PersonKindId = c.PersonKindId, PersonKindName = c.PersonKindName }).ToList();
            // bnk.BimeGozar.SodurplaceName = _context.VSodurPlace.Select(c => new ViewModel.City { CityId = c.SodurPlaceId, CityName = c.SodurPlaceName }).ToList();
            bnk.Invoice.BimekindName = _context.VBimekind.Select(c => new ViewModel.Bimekind { BimekindId = c.BimekindId, BimekindName = c.BimekindName }).ToList();
            bnk.Invoice.CountryName = _context.VCountry.Select(c => new ViewModel.Country { CountryId = (c.CountryId), CountryName = c.CountryName }).ToList();
            bnk.Invoice.CountryOutName = _context.VCountry.Select(c => new ViewModel.Country2 { CountryOutId = c.CountryId, CountryOutName = c.CountryName }).ToList();
            bnk.Invoice.CoverLimitName = _context.VCoverLimit.Select(c => new ViewModel.CoverLimit { CoverLimitId = c.CoverLimitId, CoverLimitName = c.CoverLimitName }).ToList();
            //bnk.Invoice.GrpTakhfifName = _context.VGrpTakhfif.Select(c => new ViewModel.GrpTakhfif { GrpTakhfifId = c.GrpTakhfifId, GrpTakhfifName = c.GrpTakhfifName }).ToList();
            bnk.Invoice.SodurName = _context.VLocation.Select(c => new ViewModel.Location { LocationId = c.LocationId, LocationName = c.LocationName }).ToList();
            bnk.Invoice.AgentName = _context.VLocation.Select(c => new ViewModel.Location { LocationId = c.LocationId, LocationName = c.LocationName }).ToList();
            bnk.Invoice.LocationZoneName = _context.VLocationZone.Select(c => new ViewModel.LocationZone { LocationZoneId = c.LocationZoneId, LocationZoneName = c.LocationZoneName }).ToList();
            bnk.Invoice.SafarKindName = _context.VSafarKind.Select(c => new ViewModel.SafarKind { SafarKindId = c.SafarKindId, SafarKindName = c.SafarKindName }).ToList();
            bnk.Invoice.ModdatKindName = _context.VModdatKind.Select(c => new ViewModel.ModdatKind { ModdatKindId = c.ModdatKindId, ModdatKindName = c.ModdatKindName }).ToList();
            if (id == 0)
            {
                return View("CreateOrEditTravelInvoice", bnk);
            }
            else
            {
                var policy = _context.TravelBn.SingleOrDefault(p => p.Id == id);

                TravelVM tvm = new TravelVM();
                var b = new BimeGozar
                {
                    PersonName = policy.PersonName,
                    PersonLname = policy.PersonLname,
                    PassportNo = policy.PassportNo,
                    BirthDate = policy.BirthDateMiladi,
                    FatherName = policy.FatherName,
                    Mobile = policy.Mobile,
                    PersonAddress = policy.PersonAddress,
                    CodePosti = policy.CodePosti,
                    PersonJensId = policy.PersonJens,
                    UnIranianCode = policy.UnIranianCode,
                    PersonJensName = _context.VPersonJens.Select(c => new ViewModel.PersonJens { PersonJensId = c.PersonJensId, PersonJensName = c.PersonJensName }).ToList()
                };
                var d = new Invoice
                {
                    SafarKindId = policy.SafarKind,
                    SafarKindName = _context.VSafarKind.Select(c => new ViewModel.SafarKind { SafarKindId = c.SafarKindId, SafarKindName = c.SafarKindName }).ToList(),
                    Moddat = policy.Moddat,
                    Gid = policy.Gid,
                    IssueDateMiladi = policy.IssueDateMiladi,
                    IssueDate = DateConvertor.ToShamsi(policy.IssueDateMiladi),
                    AgentId = policy.AgentCode,
                    Amount = policy.Amount,
                    AgentName = _context.VLocation.Select(c => new ViewModel.Location { LocationId = c.LocationId, LocationName = c.LocationName }).ToList(),
                    SodurId = policy.CodeSodur,
                    SodurName = _context.VLocation.Select(c => new ViewModel.Location { LocationId = c.LocationId, LocationName = c.LocationName }).ToList(),
                    CountryId = policy.Country,
                    CountryName = _context.VCountry.Select(c => new ViewModel.Country { CountryId = (c.CountryId), CountryName = c.CountryName }).ToList(),
                    CountryOutId = policy.CountryOut,
                    CountryOutName = _context.VCountry.Select(c => new ViewModel.Country2 { CountryOutId = c.CountryId, CountryOutName = c.CountryName }).ToList(),
                    CoverLimit = policy.CoverLimit,
                    CoverLimitName = _context.VCoverLimit.Select(c => new ViewModel.CoverLimit { CoverLimitId = c.CoverLimitId, CoverLimitName = c.CoverLimitName }).ToList()
                };

                tvm.BimeGozar = b;
                tvm.Invoice = d;
                return View("CreateOrEditTravelInvoice", tvm);
            }


        }

        [HttpPost]
        [PermissionChecker(15)]
        public IActionResult CreateOrEditPolicy(TravelVM tvm)
        {
            System.Globalization.PersianCalendar shamsi = new System.Globalization.PersianCalendar();

            if (tvm.Id == 0)
            {
                try
                {
                    int maxId = _context.TravelBn.Max(a => a.Id);
                    // var travelPolicy = _mapper.Map<TravelBn>(tvm);
                    var AddPolicy = new TravelBn();
                    AddPolicy.PersonName = tvm.BimeGozar.PersonName;
                    AddPolicy.PersonLname = tvm.BimeGozar.PersonLname;
                    AddPolicy.LatinName = tvm.BimeGozar.PersonName;
                    AddPolicy.LatinlName = tvm.BimeGozar.PersonLname;
                    AddPolicy.PassportNo = tvm.BimeGozar.PassportNo;
                    AddPolicy.CodePosti = tvm.BimeGozar.CodePosti;
                    AddPolicy.Mobile = tvm.BimeGozar.Mobile;
                    AddPolicy.PersonAddress = tvm.BimeGozar.PersonAddress;
                    AddPolicy.FatherName = tvm.BimeGozar.FatherName;
                    AddPolicy.UnIranianCode = tvm.BimeGozar.UnIranianCode;
                    AddPolicy.IssueDateMiladi = tvm.Invoice.IssueDateMiladi;
                    AddPolicy.IssueDate = DateConvertor.ToShamsi(tvm.Invoice.IssueDateMiladi);
                    AddPolicy.LocationZoneId = 822;//این مورد باید اصلاح شود 
                    AddPolicy.ArzType = null;
                    AddPolicy.MoenyUnitRateId = null;
                    AddPolicy.MbeginDate = null;
                    AddPolicy.Bank = null;
                    AddPolicy.FishDate = null;
                    AddPolicy.FishNo = null;
                    AddPolicy.City = null;
                    AddPolicy.CodeMelli = null;
                    AddPolicy.CompanyCode = null;
                    AddPolicy.IdentityNo = null;
                    AddPolicy.PersonKind = 46;
                    AddPolicy.GrpTakhfif = 122;
                    AddPolicy.Bimekind = 31;
                    AddPolicy.ModdatKind = 31;
                    AddPolicy.IsInFreeRegion = 0;
                    AddPolicy.Moddat = 120;
                    AddPolicy.Sodurplace = "ندارد";
                    AddPolicy.Seri = "1401";
                    AddPolicy.Nationality = 9518;
                    AddPolicy.IsSendToFan = false;
                    AddPolicy.Gid = tvm.Invoice.Gid;
                    AddPolicy.Amount = tvm.Invoice.Amount;
                    AddPolicy.Serial = (maxId + 1).ToString();
                    AddPolicy.SafarKind = tvm.Invoice.SafarKindId;
                    AddPolicy.SafarKindName = _context.VSafarKind.First(a => a.SafarKindId == tvm.Invoice.SafarKindId).SafarKindName;
                    AddPolicy.BirthDateMiladi = tvm.BimeGozar.BirthDate;
                    AddPolicy.BithDateShamsi = DateConvertor.ToShamsi(tvm.BimeGozar.BirthDate);
                    AddPolicy.IsIranian = false;
                    AddPolicy.CountryOut = tvm.Invoice.CountryOutId;
                    AddPolicy.CountryOutName = _context.VCountry.First(a => a.CountryId == tvm.Invoice.CountryOutId).CountryName;
                    AddPolicy.Country = tvm.Invoice.CountryId;
                    AddPolicy.CountryName = _context.VCountry.First(a => a.CountryId == tvm.Invoice.CountryId).CountryName;
                    AddPolicy.AgentCode = tvm.Invoice.AgentId;
                    AddPolicy.AgentName = _context.VLocation.First(a => a.LocationId == tvm.Invoice.AgentId).LocationName;
                    AddPolicy.CodeSodur = tvm.Invoice.SodurId;
                    AddPolicy.CodeSodurName = _context.VLocation.First(a => a.LocationId == tvm.Invoice.SodurId).LocationName;
                    AddPolicy.CoverLimit = tvm.Invoice.CoverLimit is null ? 10 : tvm.Invoice.CoverLimit;
                    AddPolicy.CoverLimitName = _context.VCoverLimit.First(a => a.CoverLimitId == tvm.Invoice.CoverLimit).CoverLimitName;
                    AddPolicy.DayBirth = tvm.BimeGozar.BirthDate.Day.ToString();
                    AddPolicy.MonthBirth = tvm.BimeGozar.BirthDate.Month.ToString();
                    AddPolicy.YearBirth = tvm.BimeGozar.BirthDate.Year.ToString();
                    AddPolicy.MbirthYear = tvm.BimeGozar.BirthDate.Year.ToString();
                    AddPolicy.BirthDay = shamsi.GetDayOfMonth(tvm.BimeGozar.BirthDate).ToString();
                    AddPolicy.BirthMonth = shamsi.GetMonth(tvm.BimeGozar.BirthDate).ToString("00");
                    AddPolicy.BirthYear = shamsi.GetYear(tvm.BimeGozar.BirthDate).ToString("00");
                    _context.TravelBn.Add(AddPolicy);
                    _context.SaveChanges();
                    return RedirectToAction("LoadCreateOrEdit", null);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                var EditPolicy = _context.TravelBn.FirstOrDefault(p => p.Id == tvm.Id);
                EditPolicy.PersonName = tvm.BimeGozar.PersonName;
                EditPolicy.Gid=tvm.Invoice.Gid;
                EditPolicy.PersonLname = tvm.BimeGozar.PersonLname;
                EditPolicy.LatinName = tvm.BimeGozar.PersonName;
                EditPolicy.LatinlName = tvm.BimeGozar.PersonLname;
                EditPolicy.PassportNo = tvm.BimeGozar.PassportNo;
                EditPolicy.CodePosti = tvm.BimeGozar.CodePosti;
                EditPolicy.Mobile = tvm.BimeGozar.Mobile;
                EditPolicy.PersonAddress = tvm.BimeGozar.PersonAddress;
                EditPolicy.PersonJens = tvm.BimeGozar.PersonJensId;
                EditPolicy.FatherName = tvm.BimeGozar.FatherName;
                EditPolicy.SafarKind = tvm.Invoice.SafarKindId;
                EditPolicy.IssueDateMiladi = tvm.Invoice.IssueDateMiladi;
                EditPolicy.IssueDate = DateConvertor.ToShamsi(tvm.Invoice.IssueDateMiladi);
                EditPolicy.UnIranianCode = tvm.BimeGozar.UnIranianCode;
                EditPolicy.Amount = tvm.Invoice.Amount;
                EditPolicy.SafarKindName = _context.VSafarKind.First(a => a.SafarKindId == tvm.Invoice.SafarKindId).SafarKindName;
                EditPolicy.BirthDateMiladi = tvm.BimeGozar.BirthDate;
                EditPolicy.BithDateShamsi = DateConvertor.ToShamsi(tvm.BimeGozar.BirthDate);
                EditPolicy.DayBirth = tvm.BimeGozar.BirthDate.Day.ToString();
                EditPolicy.MonthBirth = tvm.BimeGozar.BirthDate.Month.ToString();
                EditPolicy.YearBirth = tvm.BimeGozar.BirthDate.Year.ToString();
                EditPolicy.MbirthYear = tvm.BimeGozar.BirthDate.Year.ToString();
                EditPolicy.BirthDay = shamsi.GetDayOfMonth(tvm.BimeGozar.BirthDate).ToString();
                EditPolicy.BirthMonth = shamsi.GetMonth(tvm.BimeGozar.BirthDate).ToString("00");
                EditPolicy.BirthYear = shamsi.GetYear(tvm.BimeGozar.BirthDate).ToString("00");
                _context.TravelBn.Update(EditPolicy);
                _context.SaveChanges();
                return RedirectToAction("LoadCreateOrEdit", null);
            }
        }

        public IActionResult GetPolicyNotSendToFanavaran()
        {
           return View("AllCreatedPolicy");
        }

        public IActionResult LoadData()

        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                // Skiping number of Rows count  
                var start = Request.Form["start"].FirstOrDefault();
                // Paging Length 10,20  
                var length = Request.Form["length"].FirstOrDefault();
                // Sort Column Name  
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                // Sort Column Direction ( asc ,desc)  
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                //Paging Size (10,20,50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                // Getting all Customer data  
                var travel = (from a in _context.TravelBn
                              where a.IsSendToFan == false
                              select a);


                //new CustomerViewModel
                //{
                //    Id = a.Id,
                //    Amount = a.Amount,
                //    BirthDateMiladi = a.BirthDateMiladi,
                //    CodePosti = a.CodePosti,
                //    CoverLimitName = a.CoverLimitName,
                //    FatherName = a.FatherName,
                //    Gid = a.Gid,
                //    PersonName = a.PersonName,
                //    PersonLname = a.PersonLname,
                //    PersonAddress=a.PersonAddress,
                //    PassportNo=a.PassportNo,
                //    IssueDateMiladi= DateOnly.FromDateTime(a.IssueDateMiladi),
                //    IsSendToFan=a.IsSendToFan,
                //    IssueDate=a.IssueDate

                //});

                //Paging   


                ////Sorting
                //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                //{
                //    travel = travel.OrderBy(sortColumn + " " + sortColumnDirection);
                //}


                ////Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    travel = travel.Where(m => m.PassportNo == searchValue);
                }


                //total number of rows count   
                recordsTotal = travel.Count();
                //Paging   
                var data = travel.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data  
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IActionResult GetPolicySendToFanavaran()
        {
            var travel = (from a in _context.TravelBn
                          where a.IsSendToFan == true
                          select new CustomerViewModel()
                          {
                              Id = a.Id,
                              PersonName = a.PersonName,
                              PersonLname = a.PersonLname,
                              BirthDateMiladi = a.BirthDateMiladi,
                              Mobile = a.Mobile,
                              Tel = a.Tel,
                              PersonAddress = a.PersonAddress,
                              CodePosti = a.CodePosti,
                              FatherName = a.FatherName,
                              PassportNo = a.PassportNo,
                              Gid = a.Gid,
                              CoverLimitName = a.CoverLimitName
                          }).ToList();

            return View("PolicySendToFan", travel);
        }
        [PermissionChecker(19)]
        public IActionResult InquiryPolicyGet()
        {
            return View("InquiryPolicy");
        }
        public async Task<IActionResult> InquiryPolicy(string? searchString)
        {
            if (searchString == null)
            {
                ViewBag.Value = "Please Enter search value!";
                return View();
            }
            ViewData["CurrentFilter"] = searchString;
            var lst = await _context.TravelBn
                .Where(c => c.PassportNo == searchString || c.PersonName.Contains(searchString) || c.PersonLname.Contains(searchString))
                .Select(a => new CustomerViewModel
                {
                    Id = a.Id,
                    PersonName = a.PersonName,
                    PersonLname = a.PersonLname,
                    PassportNo = a.PassportNo,
                    //PersonJensName =a.PersonJens,
                    BirthDateMiladi = a.BirthDateMiladi,
                    Mobile = a.Mobile,
                    PersonAddress = a.PersonAddress,
                    FatherName = a.FatherName,
                    Gid = a.Gid,
                    CoverLimitName = a.CoverLimitName
                }).ToListAsync();
            //  string json = JsonConvert.SerializeObject(lst, Formatting.Indented);
            return Json(lst);
        }
        [PermissionChecker(17)]
        public IActionResult PrintPolicy(int id)
        {

            var pl = _context.TravelBn.FirstOrDefault(c => c.Id == id);
            PrintViewModel policy = new PrintViewModel()
            {
                Id = id,
                PersonName = pl.PersonName,
                PersonLname = pl.PersonLname,
                PassportNo = pl.PassportNo,
                PersonAddress = pl.PersonAddress,
                Mobile = pl.Mobile,
                IssueDateMiladi = pl.IssueDateMiladi,
                AgentCode = pl.AgentCode,
                BirthDate = pl.BirthDateMiladi,
                Amount = pl.Amount,
                Moddat = pl.Moddat,
                SerialNo = pl.Serial,
                CoverLimitName = pl.CoverLimitName,
            };

            return View(policy);
        }
        public async Task<IActionResult> CallFanavaranService()
        {

            #region Binding
            Uri serviceUri = new Uri("http://dfs.dayins.com/IssuedTravelService");
            EndpointAddress endpointAddress = new EndpointAddress(serviceUri);
            //Create the binding here
            BasicHttpBinding binding1 = new BasicHttpBinding();
            binding1.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            binding1.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            binding1.UseDefaultWebProxy = true;
            BasicHttpBinding binding2 = binding1;
            #endregion

            //TravelService.IssuedTravelServiceClient issuedTravel = new TravelService.IssuedTravelServiceClient(binding2, endpointAddress);
            //var Response = await issuedTravel.TravelIssuedAsync("AtbaeTravel", "T7%tyI1#654%", new TravelService.RequestTravelIssuedTravelIssuedRequest
            //{
            //    IssuingTravel = new TravelService.RequestTravelIssuedIssuingTravel
            //    {
            //        CountryCode = "",
            //        Coverage = 10,
            //        TravelKind = TravelKindEnum.Single,
            //        DepositIdentity ="123654789",
            //        Duration =120,
            //        DurationTravelKind =TravelService.DurationTravelKindEnum.Day,
            //        GregorianBirthDay ="02",
            //        GregorianBirthMonth ="07",
            //        GregorianBirthYear =1401,
            //        Insurer =,
            //        PassportNumber ="123654789",
            //        ReceiptDate ="1401/07/02",
            //        ReceiptNumber =546
            //    }
            //});




            return Ok();
        }
        [HttpPost]

        
        public async Task<IActionResult> GetReport(ReportRequest data)
        {
            TravelServices tt = new TravelServices(_context);
            CustomeActionResult<List<TravelBn>> Result = await tt.GetData(data);
            return Json(Result);
        }

        [HttpPost]
        [PermissionChecker(18)]
        public async Task<IActionResult> ExportListToExcel(ReportRequest data)
        {
            TravelServices tt = new TravelServices(_context);
            CustomeActionResult<string> Result = new CustomeActionResult<string>();


            CustomeActionResult<List<TravelBn>> ExcelData = await tt.GetDataForExcel(data);

            Result.IsSuccess = ExcelData.IsSuccess;
            Result.ResponseDesc = ExcelData.ResponseDesc;
            Result.ResponseType = ExcelData.ResponseType;
            if (Result.IsSuccess)
            {

            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new List<DataColumn> {
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
                                                  }.ToArray());

                Stopwatch timer = new Stopwatch();

                try
                {
                    foreach (TravelBn item in ExcelData.Data)
            {
                dt.Rows.Add(item.PersonName, item.PersonLname, item.PersonJens, item.PersonKind, item.CodeMelli,
                             item.CompanyCode, item.IdentityNo, item.BirthDay, item.BirthMonth, item.BirthYear,
                             item.Sodurplace, item.Mobile, item.Tel, item.PersonAddress, item.CodePosti, item.LatinName,
                             item.LatinlName, item.IsIranian, item.FatherName, item.Gid, item.UnIranianCode, item.City,
                             item.FishNo, item.FishDate, item.Bank, item.Seri, item.Serial, item.MbeginDate,
                             item.PassportNo, item.MsodurDate, item.GrpTakhfif, item.CoverLimit, item.SafarKind, item.YearBirth,
                             item.DayBirth, item.MonthBirth, item.ModdatKind, item.Moddat, item.Country, item.LocationZoneId,
                             item.ArzType, item.MoenyUnitRateId, item.MbirthYear, item.CodeSodur, item.AgentCode, item.Bimekind, item.IsInFreeRegion
                             );

                    }

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        using (MemoryStream st = new MemoryStream())
                        {
                            wb.SaveAs(st);
                           // return File(st.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheatml.sheet", "Grid.xlsx");
                            Result.Data = Convert.ToBase64String(st.ToArray());
                        }
                    }

                    CustomeActionResult updateDataResult = await tt.UpdateDataForExcel(data.SelectedId);
                    Result.IsSuccess = updateDataResult.IsSuccess;
                    Result.ResponseDesc = updateDataResult.ResponseDesc;
                    Result.ResponseType = updateDataResult.ResponseType;

                }
                catch (Exception ex)
                {
                    Result.ResponseDesc = ex.Message;
                    Result.IsSuccess = false;
                }
            }


            return Json(Result);
        }

    }
}
