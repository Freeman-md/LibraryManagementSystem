using System;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Tests.TestHelpers;

namespace LibraryManagementSystem.Tests.UnitTests.Services.BookService
{
    public class AddBookTests : IClassFixture<BookServiceFixture>
    {
        private readonly LibraryManagementSystem.Services.BookService _bookService;

        public AddBookTests(BookServiceFixture fixture)
        {
            _bookService = fixture.BookService;
        }

        private static Book CreateBook(
            string title = "Original Title",
            string author = "Original Author",
            string genre = "Original Genre",
            string isbn = "090-93080-3893",
            DateTime publishDate = default(DateTime),
            bool isAvailable = true) => new Book(title, author, genre, isbn, publishDate, isAvailable);

        [Fact]
        public void AddBook_WithNullItem_ShouldThrowArgumentNullException()
        {
            Book? nullBook = null;

            Assert.Throws<ArgumentNullException>(() => _bookService.AddBook(nullBook));
        }

        [Fact]
        public void AddBook_WithEmptyTitle_ShouldThrowArgumentException()
        {
            Book invalidBook = CreateBook(title: "");

            ArgumentException ex = Assert.Throws<ArgumentException>(() => _bookService.AddBook(invalidBook));
            Assert.Equal("Title", ex.ParamName);
        }

        [Fact]
        public void AddBook_WithEmptyAuthor_ShouldThrowArgumentException()
        {
            Book invalidBook = CreateBook(author: "");

            ArgumentException ex = Assert.Throws<ArgumentException>(() => _bookService.AddBook(invalidBook));
            Assert.Equal("Author", ex.ParamName);
        }

        [Fact]
        public void AddBook_WithEmptyGenre_ShouldThrowArgumentException()
        {
            Book invalidBook = CreateBook(genre: "");

            ArgumentException ex = Assert.Throws<ArgumentException>(() => _bookService.AddBook(invalidBook));
            Assert.Equal("Genre", ex.ParamName);
        }

        [Fact]
        public void AddBook_WithEmptyISBN_ShouldThrowArgumentException()
        {
            Book invalidBook = CreateBook(isbn: "");

            ArgumentException ex = Assert.Throws<ArgumentException>(() => _bookService.AddBook(invalidBook));
            Assert.Equal("ISBN", ex.ParamName);
        }

        [Fact]
        public void AddBook_WithAllDetails_ShouldAddItemSuccessfully()
        {
            Book book = CreateBook();

            _bookService.AddBook(book);
            Book addedBook = _bookService.GetBookById(book.Id);

            Assert.NotNull(addedBook);
            Assert.Equal(book.Id, addedBook.Id);
            Assert.Equal(book.Author, addedBook.Author);
            Assert.Equal(book.Genre, addedBook.Genre);
            Assert.Equal(book.ISBN, addedBook.ISBN);
            Assert.Equal(DateTime.Now.Date, addedBook.PublishDate.Date);
            Assert.True(addedBook.IsAvailable);
        }
    }
}

