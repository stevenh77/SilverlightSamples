namespace LibrarySystem.Interfaces
{
    public interface IPublication
    {
        string Author { get; }
        string ISBN { get; }
        IPublisher Publisher { get; }
        string Title { get; }
    }
}