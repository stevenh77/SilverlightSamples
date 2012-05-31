using System;
using LibrarySystem.Core;
using NUnit.Framework;

namespace LibrarySystem.Tests.Core
{
    [TestFixture]
    class PersonTests
    {
        [Test]
        public void Create_WithValidParams_ShouldReturnPerson()
        {
            var person = Person.Create("dummy", "dummy");
            Assert.IsNotNull(person);
        }

        [Test]
        public void Create_WithNullAddress_ShouldThrowAgrumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Person.Create(null, "dummy"));   
        }
        
        [Test]
        public void Create_WithNullName_ShouldThrowAgrumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Person.Create("dummy", null));
        }

        [Test]
        public void Create_WithNullAddressAndName_ShouldThrowAgrumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Person.Create(null, null));
        }
    }
}
