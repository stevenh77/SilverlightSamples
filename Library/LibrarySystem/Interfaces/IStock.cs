namespace LibrarySystem.Interfaces
{
    public interface IStock
    {
        bool IsAvailable { get; set; }
        IBook Book { get; }
    }
}