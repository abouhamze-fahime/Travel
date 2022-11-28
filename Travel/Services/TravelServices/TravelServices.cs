using Microsoft.EntityFrameworkCore;
using System.Data;
using Travel.Models.Travel;
using Travel.TravelDbContext;
using Travel.ViewModel;

namespace Travel.Services.TravelServices
{
    public class TravelServices
    {


        private readonly TravelContext _context;
    
        public TravelServices(TravelContext context)
        {
            _context = context;
        }
        public async Task<CustomeActionResult<List<TravelBn>>> GetData(ReportRequest data)
        {


            CustomeActionResult<List<TravelBn>> Result = new CustomeActionResult<List<TravelBn>>();
            try
            {
                var lst = from a in _context.TravelBn
                          where a.IssueDateMiladi >= data.FromDate && a.IssueDateMiladi <= data.ToDate
                          
                          select a;
                {
                    Result.Data = await lst.ToListAsync();
                    Result.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {

                Result.IsSuccess = false;
                Result.ResponseDesc = ex.Message;
            }

            //if (Result.IsSuccess && data.SelectedId != null && data.SelectedId.Any()) //
            //{
            //    Result.Data = Result.Data.Where(x => data.SelectedId.Contains(x.Id)).ToList();
            //}
            return Result;
        }

        public async Task<CustomeActionResult<List<TravelBn>>> GetDataForExcel(ReportRequest data)
        {


            CustomeActionResult<List<TravelBn>> Result = await GetData(data);
                //new CustomeActionResult<List<TravelBn>>();
            

            if (Result.IsSuccess && data.SelectedId != null && data.SelectedId.Count() != 0) //
            {
                Result.Data = Result.Data.Where(x => data.SelectedId.Contains(x.Id)).ToList();
            }
            return Result;
        }



        public async Task<CustomeActionResult> UpdateDataForExcel(List<int> Id)
        {
            CustomeActionResult Result = new CustomeActionResult();
            try
            {

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Id");
                foreach (var item in Id)
                {
                    var policy= await _context.TravelBn.FirstOrDefaultAsync(p => p.Id == item);
                    policy.IsSendToFan = true;
                    dataTable.Rows.Add(item);
                    _context.TravelBn.Update(policy);


                }
               await _context.SaveChangesAsync();

                    Result.IsSuccess = true;
               
            }
            catch (Exception ex)
            {

                Result.IsSuccess = false;
                Result.ResponseDesc = ex.Message;
            }
            return Result;


        }
    }
}
