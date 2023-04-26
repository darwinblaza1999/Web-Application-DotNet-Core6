using WatchWebApp.Repository;
using WatchWebApp.Repository.UnitofWork;

namespace WatchWebApp.Class.Core
{
    public class AdpterClass : IAdapter
    {
        public IWatch watch { get; }
        public AdpterClass()
        {
            this.watch = new WatchClass();
        }
    }
}
