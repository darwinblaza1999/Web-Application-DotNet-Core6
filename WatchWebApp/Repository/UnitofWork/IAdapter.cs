namespace WatchWebApp.Repository.UnitofWork
{
    public interface IAdapter
    {
        IWatch watch { get; }
        IToken token { get; }
        IBlob blob { get; }
    }
}
