using System;
using System.Collections.Generic;
using LibrarySystem.Core;
using LibrarySystem.Interfaces;
using NUnit.Framework;

namespace LibrarySystem.Tests.Core
{
    [TestFixture]
    public class LibraryTests
    {
        #region Factory methods to setup fixed scenario for testing
        
        // Factory that creates:
        //      A library object with 10 books (2 x Lord of the Flies, 3 x 1984, 5 x Brave New World)
        private ILibrary CreateLibraryScenario()
        {
            #region Publisher

            IPublisher penguin = Publisher.Create("Penguin");
            
            #endregion

            #region Publications

            IPublication lordOfTheFlies = Publication.Create("William Golding", "0140283331", penguin, "Lord of the Flies");

            IPublication nineteenEightyFour = Publication.Create("George Orwell", "0452284236", penguin, "Nineteen Eighty-Four");

            IPublication braveNewWorld = Publication.Create("Aldous Huxley", "0060850523", penguin, "Brave New World");

            #endregion

            #region Books

            // Lord of the flies
            IBook book1 = Book.Create("0000001", lordOfTheFlies);
            IBook book2 = Book.Create("0000002", lordOfTheFlies);
            IBook book3 = Book.Create("0000003", lordOfTheFlies);

            // 1984
            IBook book4 = Book.Create("0000004", nineteenEightyFour);
            IBook book5 = Book.Create("0000005", nineteenEightyFour); 

            // Brave new world
            IBook book6 = Book.Create("0000006", braveNewWorld);
            IBook book7 = Book.Create("0000007", braveNewWorld);
            IBook book8 = Book.Create("0000008", braveNewWorld);
            IBook book9 = Book.Create("0000009", braveNewWorld);
            IBook book10 = Book.Create("0000010", braveNewWorld);

            #endregion
            
            #region Library

            ILibrary library = Library.Create("Leicester Square");
            library.AddStock(book1, book2, book3, book4, book5, book6, book7, book8, book9, book10);

            #endregion

            return library;
        }

        public IBook CreateBook(string bookId = "dummy")
        {
            IPublisher publisher = Publisher.Create("dummy");
            IPublication publication = Publication.Create("dummy", "dummy", publisher, "dummy");
            IBook book = Book.Create(bookId, publication);
            return book;
        }

        #endregion

        #region Create

        [Test]
        public void Create_WithNullLocation_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Library.Create(null));
        }

        #endregion

        #region AddStock

        [Test]
        public void AddStock_WithNullBook_ShouldThrowArgumentNullException()
        {
            var library = CreateLibraryScenario();
            Assert.Throws<ArgumentNullException>(() => library.AddStock(null));
        }

        [Test]
        public void AddStock_WithOneValidBookAndOneNullBook_ShouldThrowArgumentNullException()
        {
            var library = CreateLibraryScenario();
            var validBook = CreateBook();
            Assert.Throws<ArgumentNullException>(() => library.AddStock(validBook, null));
        }

        [Test]
        public void AddStock_WithValidBook_ShouldIncreaseStockListCountByOne()
        {
            //arrange
            var library = CreateLibraryScenario();
            var expected = library.StockList.Count + 1;
            var dummyBook = CreateBook();

            //act
            library.AddStock(dummyBook);
            var actual = library.StockList.Count;

            //assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddStock_WithBookIdAlreadyInStock_ShouldThrowException()
        {
            //arrange
            var library = CreateLibraryScenario();
            var idThatIsAlreadyInStock = "0000001";
            var dummyBook = CreateBook(idThatIsAlreadyInStock);

            //act
            Assert.Throws<Exception>(
                () => library.AddStock(dummyBook), 
                string.Format("Book with {0} is already in stock", idThatIsAlreadyInStock));
        }

        [Test]
        public void AddStock_WithValidBook_ShouldMakeBookAvailable()
        {
            //arrange
            var library = CreateLibraryScenario();
            var id = "0000001";
            var dummyBook = CreateBook();

            //act
            library.AddStock(dummyBook);
            var lookupBook = ((List<IStock>) library.StockList).Find(s => s.Book.Id == id);
            var actual = lookupBook.IsAvailable;

            //assert
            Assert.IsTrue(actual);
        }

        #endregion

        #region IsBookAvailableByISBN

        [Test]
        public void IsBookAvailableByISBN_WithNullISBN_ShouldThrowArgumentNullExpection()
        {
            var library = CreateLibraryScenario();
            Assert.Throws<ArgumentNullException>(() => library.IsBookAvailableByISBN(null));
        }

        [Test]
        public void IsBookAvailableByISBN_WithValidISBN_ShouldReturnTrue()
        {
            var library = CreateLibraryScenario();

            // Lord of the flies
            var validISBN = "0140283331";

            var actual = library.IsBookAvailableByISBN(validISBN);

            Assert.IsTrue(actual);
        }

        [Test]
        public void IsBookAvailableByISBN_WithUnknownISBN_ShouldReturnFalse()
        {
            var library = CreateLibraryScenario();

            // rubbish data
            var unknownISBN = "X";

            var actual = library.IsBookAvailableByISBN(unknownISBN);

            Assert.IsFalse(actual);
        }

        #endregion

        #region FindBooks

        [Test]
        public void FindBooks_WhenNullSearchString_ShouldThrowArgumentNullExpection()
        {
            var library = CreateLibraryScenario();
            Assert.Throws<ArgumentNullException>(() => library.FindBooks(null));
        }


        [Test]
        public void FindBooks_WithValidSearchTitleString_ShouldReturnCorrectNumberOfBooks()
        {
            var library = CreateLibraryScenario();

            // good search string
            var searchString = "Flies";

            // number of Lord of the Flies books expected
            var expected = 3;
            var actual = library.FindBooks(searchString).Count;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FindBooks_WithValidSearchTitleStringLowerCase_ShouldReturnCorrectNumberOfBooks()
        {
            var library = CreateLibraryScenario();

            // good search string
            var searchString = "flies";

            // number of Lord of the Flies books expected
            var expected = 3;
            var actual = library.FindBooks(searchString).Count;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FindBooks_WithValidSearchStringMultiplePublications_ShouldReturnCorrectNumberOfBooks()
        {
            var library = CreateLibraryScenario();

            // good search string
            var searchString = "e";

            // number of Lord of the Flies books expected
            var expected = 10;
            var actual = library.FindBooks(searchString).Count;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FindBooks_WithValidSearchAuthorString_ShouldReturnCorrectNumberOfBooks()
        {
            var library = CreateLibraryScenario();

            // number of Brave New World books expected
            var expected = 5;
            var searchString = "ldous";

            var actual = library.FindBooks(searchString).Count;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void FindBooks_WithInvalidSearchString_ShouldBeEmpty()
        {
            var library = CreateLibraryScenario();

            // rubbish data
            var searchString = "XYZ";

            var actual = library.FindBooks(searchString);

            Assert.IsEmpty(actual);
        }

        #endregion

        #region GetBookById

        [Test]
        public void GetBookById_WhenNullId_ShouldThrowArgumentNullExpection()
        {
            var library = CreateLibraryScenario();
            Assert.Throws<ArgumentNullException>(() => library.GetBookById(null));
        }

        [Test]
        public void GetBookById_WhenValidId_ShouldReturnBook()
        {
            var library = CreateLibraryScenario();
            var validBookId = "0000001";
            var book = library.GetBookById(validBookId);
            Assert.IsNotNull(book);
        }

        [Test]
        public void GetBookById_WhenValidId_ShouldReturnNull()
        {
            var library = CreateLibraryScenario();
            var invalidBookId = "X";
            var book = library.GetBookById(invalidBookId);
            Assert.IsNull(book);
        }

        #endregion

        #region LendBook

        [Test]
        public void LendBook_ShouldMakeBookInLibraryStockListUnavailable()
        {
            //arrange 
            var library = CreateLibraryScenario();
            var person = CreatePerson();
            var bookId = "0000001";

            // act
            library.LendBook(person, bookId);
            var lookupBook = ((List<IStock>) library.StockList).Find(s => s.Book.Id == bookId);
            var actual = lookupBook.IsAvailable;

            // assert
            Assert.IsFalse(actual);
        }

        private IPerson CreatePerson()
        {
            var person = Person.Create("dummy", "dummy");
            return person;
        }

        [Test]
        public void LendBook_WithNullPerson_ShouldThrowArgumentNullException()
        {
            //arrange 
            var library = CreateLibraryScenario();
            var id = "dummy";

            // act & assert
            Assert.Throws<ArgumentNullException>(() => library.LendBook(null, id));
        }

        [Test]
        public void LendBook_WithNullBook_ShouldThrowArgumentNullException()
        {
            //arrange 
            var library = CreateLibraryScenario();
            var person = CreatePerson();

            // act & assert
            Assert.Throws<ArgumentNullException>(() => library.LendBook(person, null));
        }

        [Test]
        public void LendBook_WithNullBookAndNullPerson_ShouldThrowArgumentNullException()
        {
            //arrange 
            var library = CreateLibraryScenario();

            // act & assert
            Assert.Throws<ArgumentNullException>(() => library.LendBook(null, null));
        }

        [Test]
        public void LendBook_WithUnknownBook_ShouldThrowArgumentException()
        {
            //arrange 
            var library = CreateLibraryScenario();
            var person = CreatePerson();
            var id = "dummy";

            // act & assert
            Assert.Throws<Exception>(
                () => library.LendBook(person, id), 
                string.Format("Book id {0} is not in stock", id));
        }

        #endregion
        
    }
}
