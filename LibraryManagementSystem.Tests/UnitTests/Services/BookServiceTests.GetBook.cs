using System;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Tests.TestHelpers;

namespace LibraryManagementSystem.Tests.UnitTests.Services
{
    public partial class BookServiceTests : IClassFixture<BookServiceFixture>
    {
        [Fact]
        public void GetBook_WhenBookIdIsValid_ShouldReturnBook()
        {
            Book book = _bookService.AddBook(CreateBook());

            Book? foundBook = _bookService.GetBookById(book.Id);

            Assert.NotNull(foundBook);
            Assert.Equal(book.Title, foundBook.Title);
        }

        [Fact]
        public void GetBook_WhenBookDoesNotExist_ShouldReturnNull()
        {
            Book book = CreateBook();

            Book? foundBook = _bookService.GetBookById(book.Id);

            Assert.Null(foundBook);
        }
    }
}

