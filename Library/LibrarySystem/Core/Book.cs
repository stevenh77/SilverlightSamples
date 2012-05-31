using System;
using LibrarySystem.Interfaces;

namespace LibrarySystem.Core
{
    public class Book : IBook
    {
        public string Author { get; set; }
        public string ISBN { get; set; }
        public IPublisher Publisher { get; set; }
        public string Title { get; set; }
        public string Id { get; set; }

        private Book()
        {
            
        }

        public static IBook Create(string id, IPublication publication)
        {
            if (id == null || publication == null)
                throw new ArgumentNullException();

            return new Book()
                       {
                           Author = publication.Author,
                           Id = id,
                           ISBN = publication.ISBN,
                           Title = publication.Title,
                           Publisher = publication.Publisher
                       };
        }
    }
}
