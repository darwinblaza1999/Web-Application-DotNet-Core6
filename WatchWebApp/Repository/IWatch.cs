using System.Data;
using WatchWebApp.Models;

namespace WatchWebApp.Repository
{
    public interface IWatch
    {
        Task<Response<object>> AddNewItem(WatchModel model);
        Task<Response<object>> UpdateItem(WatchModel2 model);
        Task<Response<string>> GetById(int Id);
        Task<Response<string>> GetAllItem();
        Task<Response<object>> DeleteItem(int id);
        Task<Response<object>> UploadImageBlob(IFormFile file);
    }
}
