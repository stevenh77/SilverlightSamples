using System;
using LibrarySystem.Core;
using NUnit.Framework;

namespace LibrarySystem.Tests.Core
{
    [TestFixture]
    class PublicationTests
    {
        [Test]
        public void Create_WithValidParams_ShouldReturnPublication()
        {
            var publisher = Publisher.Create("dummy");
            var publication = Publication.Create("dummy","dummy", publisher, "dummy");
            Assert.IsNotNull(publication);
        }

        [Test]
        public void Create_WithNullAuthor_ShouldThrowAgrumentNullException()
        {
            var publisher = Publisher.Create("dummy");
            Assert.Throws<ArgumentNullException>(() => Publication.Create(null, "dummy", publisher, "dummy"));
        }

        [Test]
        public void Create_WithNullISBN_ShouldThrowAgrumentNullException()
        {
            var publisher = Publisher.Create("dummy");
            Assert.Throws<ArgumentNullException>(() => Publication.Create("dummy", null, publisher, "dummy"));
        }

        [Test]
        public void Create_WithNullPublisher_ShouldThrowAgrumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Publication.Create("dummy","dummy", null, "dummy"));
        }

        [Test]
        public void Create_WithNullTitle_ShouldThrowAgrumentNullException()
        {
            var publisher = Publisher.Create("dummy");
            Assert.Throws<ArgumentNullException>(() => Publication.Create("dummy", "dummy", publisher, null));
        }
    }
}
