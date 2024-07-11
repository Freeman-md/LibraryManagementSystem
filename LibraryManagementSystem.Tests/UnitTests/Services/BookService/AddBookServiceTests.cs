using System;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Tests.TestHelpers;

namespace LibraryManagementSystem.Tests.UnitTests.Services.BookService
{
    public class AddBookServiceTests : IClassFixture<BookServiceFixture>
    {
        private readonly LibraryManagementSystem.Services.BookService _bookService;

        public AddBookServiceTests(BookServiceFixture fixture) {
            _bookService = fixture.BookService;
        }

		[Fact]
		public void AddBook_WithNoArguments_ShouldAddBookWithDefaultIDAndPublishDateOnly()
		{
            Book book = new Book();

            _bookService.AddBook(book);
            List<Book> allBooks = _bookService.GetBooks();
            Book addedBook = allBooks.FirstOrDefault(iterable => iterable.Id == book.Id);

            Assert.NotNull(addedBook);
            Assert.Equal(book.Id, addedBook.Id);
            Assert.Equal(DateTime.Now.Date, addedBook.PublishDate.Date);
            Assert.True(addedBook.IsAvailable);
            Assert.Null(addedBook.Author);
            Assert.Null(addedBook.Genre);
            Assert.Null(addedBook.ISBN);
        }

        [Fact]
        public void AddBook_WithValidBook_ShouldAddItemSuccessfully()
        {
            Book book = new Book("New Author", "New Genre", "090-3829-3838");

            _bookService.AddBook(book);
            List<Book> allBooks = _bookService.GetBooks();
            Book addedBook = allBooks.FirstOrDefault(iterable => iterable.Id == book.Id);

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

