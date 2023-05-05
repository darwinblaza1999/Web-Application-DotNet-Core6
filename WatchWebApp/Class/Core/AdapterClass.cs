using Microsoft.Extensions.Options;
using WatchWebApp.Options;
using WatchWebApp.Repository;
using WatchWebApp.Repository.UnitofWork;

namespace WatchWebApp.Class.Core
{
    public class AdapterClass : IAdapter
    {
        public IWatch watch { get; }
        public IToken token { get; }
        public IBlob blob { get; }

        public AdapterClass(IOptions<AppSettings> settings)
        {
            this.watch = new WatchClass(settings);
            token = new TokenClass(settings);
            blob = new BlobClass(settings);
        }
    }
}
