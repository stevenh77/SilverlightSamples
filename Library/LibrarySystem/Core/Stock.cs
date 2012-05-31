using System;
using LibrarySystem.Interfaces;

namespace LibrarySystem.Core
{
    public class Stock : IStock
    {
        public IBook Book { get; private set; }
        public bool IsAvailable { get; set; }

        private Stock()
        {
            
        }

        public static IStock Create(IBook book, bool isAvailable)
        {
            if (book == null)
                throw new ArgumentNullException();

            return new Stock() {Book = book, IsAvailable = isAvailable};
        }
    }
}
