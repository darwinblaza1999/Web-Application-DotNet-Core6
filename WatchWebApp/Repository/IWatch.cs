using System.Data;
using WatchWebApp.Models;

namespace WatchWebApp.Repository
{
    public interface IWatch
    {
        Task<Response<string>> AddNewItem(WatchModel model, string token);
        Task<Response<string>> UpdateItem(WatchModel2 model, string token);
        Task<Response<string>> GetById(int Id, string token);
        Task<Response<string>> GetAllItem(string token);
        Task<Response<object>> DeleteItem(int id, string token);
        Task<Response<string>> UpdateImage(WatchImage model, string token);
    }
}
