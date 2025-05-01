namespace WpfClient.Utilities
{
    public interface ICloseable
    {
        event Action<bool?> CloseRequested;
    }
}
