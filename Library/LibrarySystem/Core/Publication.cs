using System;
using LibrarySystem.Interfaces;

namespace LibrarySystem.Core
{
    public class Publication : IPublication
    {
        public string Author { get; private set; }
        public string ISBN { get; private set; }
        public IPublisher Publisher { get; private set; }
        public string Title { get; private set; }

        private Publication()
        {
            
        }

        public static IPublication Create(string author, string isbn, IPublisher publisher, string title)
        {
            if (author == null || isbn == null || publisher == null || title == null)
                throw new ArgumentNullException();

            return new Publication()
                       {
                           Author = author,
                           ISBN = isbn,
                           Publisher = publisher,
                           Title = title
                       };
        }
    }

}
