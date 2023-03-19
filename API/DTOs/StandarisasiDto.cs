namespace API.DTOs
{
    public class StandarisasiDto<T>
    {
        public string status { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public T Data { get; set; }
    }
}