namespace Travel.ViewModel
{
    public class CustomeActionResult<T>
    {
        public bool IsSuccess { get; set; }
        public string ResponseDesc { get; set; }
        public int ResponseType { get; set; }
        public T Data { get; set; }
    }

    public class CustomeActionResult
    {
        public bool IsSuccess { get; set; }
        public string ResponseDesc { get; set; }
        public int ResponseType { get; set; }
    }
}
