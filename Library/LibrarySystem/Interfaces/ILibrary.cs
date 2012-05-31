using System.Collections.Generic;

namespace LibrarySystem.Interfaces
{
    public interface ILibrary
    {
        string Location { get; }
        IList<IStock> StockList { get; }

        void AddStock(params IBook[] books);
        bool IsBookAvailableByISBN(string isbn);
        IList<IStock> FindBooks(string searchString);
        IBook GetBookById(string id);
        void LendBook(IPerson person, string id);
    }
}