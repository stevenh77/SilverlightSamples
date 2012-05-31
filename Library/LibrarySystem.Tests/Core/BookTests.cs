using System;
using LibrarySystem.Core;
using LibrarySystem.Interfaces;
using NUnit.Framework;

namespace LibrarySystem.Tests.Core
{
    [TestFixture]
    class BookTests
    {
        [Test]
        public void Create_WithNullIdAndNullPublication_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Book.Create(null, null));
        }

        [Test]
        public void Create_WithNullIdAndValidPublication_ShouldThrowArgumentNullException()
        {
            var publisher = Publisher.Create("dummy");
            var publication = Publication.Create("dummy", "dummy", publisher, "dummy");
            Assert.Throws<ArgumentNullException>(() => Book.Create(null, publication));
        }

        [Test]
        public void Create_WithNullPublication_ShouldThrowArgumentNullException()
        {
            var validId = "1";
            Assert.Throws<ArgumentNullException>(() => Book.Create(validId, null));
        }

        [Test]
        public void Create_WithValidParametersPublication_ShouldReturnBook()
        {
            var publisher = Publisher.Create("dummy");
            var publication = Publication.Create("dummy","dummy", publisher, "dummy");
            var validId = "1";
            IBook book = Book.Create(validId, publication);
            Assert.NotNull(book);
        }
    }
}