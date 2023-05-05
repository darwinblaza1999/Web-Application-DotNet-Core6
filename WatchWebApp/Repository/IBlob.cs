using WatchWebApp.Models;

namespace WatchWebApp.Repository
{
    public interface IBlob
    {
        Task<Response<string>> UploadImage(IFormFile file, string token);
        Task<Response<string>> DeleteImage(string imageUrl, string token);
    }
}
