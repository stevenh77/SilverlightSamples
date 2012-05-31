namespace LibrarySystem.Interfaces
{
    public interface IBook : IPublication
    {
        string Id { get; }
    }
}