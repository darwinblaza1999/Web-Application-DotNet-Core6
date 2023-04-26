namespace WatchWebApp.Models
{
    public class Response<T>
    {
        public bool isSuccess { get; set; }
        public bool isException { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}
