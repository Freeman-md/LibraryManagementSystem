using System;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Tests.UnitTests.Services
{
	public partial class BookServiceTests
	{
		[Fact]
		public void UpdateBook_ShouldUpdateBookSuccessfully()
		{
			Book book = CreateBook();
			_bookService.AddBook(book);
			Book updatedBook = CreateBook(title: "Updated Title", author: "Updated Author");

			_bookService.UpdateBook(updatedBook, book.Id);
			Book? result = _bookService.GetBookById(book.Id);

			Assert.NotNull(result);
			Assert.Equal(updatedBook.Title, result.Title);
            Assert.Equal(updatedBook.Author, result.Author);
        }

		[Fact]
        public void UpdateBook_WithNullItem_ShouldThrowArgumentNullException()
        {
            Book book = CreateBook();
            _bookService.AddBook(book);
            Book? updatedBook = null;

			Assert.Throws<ArgumentNullException>(() => _bookService.UpdateBook(updatedBook!, book.Id));
        }

        [Fact]
        public void UpdateBook_WhenBookDoesNotExist_ShouldThrowArgumentException()
        {
            Guid id = Guid.NewGuid();
            Book updatedBook = CreateBook();

            Assert.Throws<ArgumentException>(() => _bookService.UpdateBook(updatedBook, id));
        }
    }
}

