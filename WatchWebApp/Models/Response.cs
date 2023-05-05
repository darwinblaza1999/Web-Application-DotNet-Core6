namespace WatchWebApp.Models
{
    public class Response<T>
    {
        public bool isSuccess { get; set; } = false;
        public bool isException { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
    public class APIResponse<T>
    {
        public ResponseStatusCode HttpCode { get; set; }
        public string? DeveloperMessage { get; set; }
        public dynamic Code { get; set; }
        public T? Data { get; set; }
    }
    public enum ResponseStatusCode
    {
        Success = 200,
        BadRequest = 400,
        InternalError = 500,
        Unauthorized = 401
    }
    public class TokenResponse<T>
    {
        public int code { get; set; }
        public string? message { get; set; }
        public T? data { get; set; }
        public string? token { get; set; }
    }
}
