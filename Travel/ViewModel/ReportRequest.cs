namespace Travel.ViewModel
{
    public class ReportRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public DateTime? FromDateTime
        {
            get
            {

                if (FromDate== null)
                {
                    return null;

                }
                else
                {


                    return  (FromDate);

                }
            }
        }
        public DateTime? ToDateTime
        {
            get
            {
                if (ToDate==null)
                {
                    return null;

                }
                else
                {

                    
                    return ToDate;

                }
            }
        }
        public List<int> SelectedId { get; set; }
    }
}
