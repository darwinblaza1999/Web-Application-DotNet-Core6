using WatchWebApp.Models;

namespace WatchWebApp.Repository
{
    public interface IToken
    {
        Task<Response<string>> GetToken();
    }
}
