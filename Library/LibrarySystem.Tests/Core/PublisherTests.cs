using System;
using LibrarySystem.Core;
using NUnit.Framework;

namespace LibrarySystem.Tests.Core
{
    [TestFixture]
    class PublisherTests
    {
        [Test]
        public void Create_WithValidName_ShouldReturnPublisher()
        {
            var publisher = Publisher.Create("dummy");
            Assert.IsNotNull(publisher);
        }

        [Test]
        public void Create_WithNullName_ShouldThrowAgrumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Publisher.Create(null));
        }
    }
}
