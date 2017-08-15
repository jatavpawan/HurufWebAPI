namespace Huruf.BAL
{
    public class ResponseData
    {
        public string Status { get; set; }
        public dynamic Data { get; set; }
        public string Error { get; set; }
        public string ErrorCode { get; set; }
        public string Message { get; set; }
    }
}
