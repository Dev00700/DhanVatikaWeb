namespace DhanVatikaWeb.Models
{
    public class CommonResponseDto<T>
    {
        public int PageSize { get; set; }
        public int PageRecordCount { get; set; }
        public int? TotalRecordCount { get; set; }
        public T Data { get; set; } 
        public int Flag { get; set; }
        public string Message { get; set; }
    }
}
