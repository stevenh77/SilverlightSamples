using System;
using LibrarySystem.Core;
using NUnit.Framework;

namespace LibrarySystem.Tests.Core
{
    [TestFixture]
    class StockTests
    {
        [Test]
        public void Create_WithValidName_ShouldReturnPublisher()
        {
            var publisher = Publisher.Create("dummy");
            var publication = Publication.Create("dummy","dummy", publisher, "dummy");
            var book = Book.Create("dummy", publication);
            var stock = Stock.Create(book, true);
            Assert.IsNotNull(stock);
        }

        [Test]
        public void Create_WithNullBook_ShouldThrowAgrumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Stock.Create(null, true));
        }
    }
}
