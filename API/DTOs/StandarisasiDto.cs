namespace API.DTOs
{
    public class StandarisasiDto<T>
    {

        public StandarisasiDto()
        {
            
        }
        public StandarisasiDto(string status, int statusCode, string message, T data)
        {
            Status = status;
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }
        public string Status { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public T Data { get; set; }
    }
}