using System;
using System.Collections.Generic;
using System.Linq;
using LibrarySystem.Interfaces;

namespace LibrarySystem.Core
{
    public class Library : ILibrary
    {
        public string Location { get; private set; }
        public IList<IStock> StockList { get; private set; }

        private Library() 
        {
        }

        public static ILibrary Create(string location)
        {
            if (location == null)
                throw new ArgumentNullException();

            return new Library() {Location = location, StockList = new List<IStock>(0)};
        }

        public void AddStock(params IBook[] books)
        {
            if (books == null)
                throw new ArgumentNullException();

            var stock = new List<IStock>(StockList.Count + books.Length);

            stock.AddRange(StockList);

            foreach (var book in books)
            {
                if (book == null)
                    throw new ArgumentNullException();

                ThrowExceptionIfBookIsAlreadyInStock(book);

                stock.Add(Stock.Create(book, true));
            }

            StockList = stock;
        }

        private void ThrowExceptionIfBookIsAlreadyInStock(IBook book)
        {
            var lookup = ((List<IStock>) StockList).Find(s => s.Book.Id == book.Id);
            if (lookup != null)
                throw new Exception(string.Format("Book id {0} is already in stock", book.Id));
        }

        public bool IsBookAvailableByISBN(string isbn)
        {
            if (isbn == null)
                throw new ArgumentNullException();

            var qry = from books in StockList
                      where books.Book.ISBN == isbn 
                            & books.IsAvailable
                      select books.IsAvailable;

            return qry.FirstOrDefault();
        }

        public IList<IStock> FindBooks(string searchString)
        {
            if (searchString == null)
                throw new ArgumentNullException();

            searchString = searchString.ToLower();

            var qry = from books in StockList
                      where books.Book.Title.ToLower().Contains(searchString)
                            || books.Book.Author.ToLower().Contains(searchString)
                      select books;

            return qry.ToList();
        }

        public IBook GetBookById(string id)
        {
            if (id == null)
                throw new ArgumentNullException();

            id = id.ToLower();

            var qry = from books in StockList
                      where books.Book.Id.ToLower() == id
                      select books.Book;

            return qry.FirstOrDefault();
        }


        public void LendBook(IPerson person, string id)
        {
            if (person == null || id == null)
                throw new ArgumentNullException();

            var stock = ThrowExceptionIfBookIsNotInStock(id);
            stock.IsAvailable = false;

            // FUTURE: register with person that they have borrowed the book
        }

        private IStock ThrowExceptionIfBookIsNotInStock(string id)
        {
            var lookup = ((List<IStock>) StockList).Find(s => s.Book.Id == id);
            if (lookup == null)
                throw new Exception(string.Format("Book with id {0} is not in stock", id));
            return lookup;
        }
    }
}
