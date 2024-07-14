using System;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Tests.TestHelpers;

namespace LibraryManagementSystem.Tests.UnitTests.Services
{
    public partial class BookServiceTests : IClassFixture<BookServiceFixture>
    {
        [Fact]
        public void AddBook_WithNullItem_ShouldThrowArgumentNullException()
        {
            Book? nullBook = null;

            Assert.Throws<ArgumentNullException>(() => _bookService.AddBook(nullBook!));
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

            Book addedBook = _bookService.AddBook(book);

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

