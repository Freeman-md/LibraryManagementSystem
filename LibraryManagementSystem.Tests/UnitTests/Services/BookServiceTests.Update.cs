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
        public void UpdateBook_WithEmptyTitle_ShouldThrowArgumentException()
        {
            Book book = CreateBook();
            _bookService.AddBook(book);

            Book invalidBook = CreateBook(title: "");

			ArgumentException ex = Assert.Throws<ArgumentException>(() => _bookService.UpdateBook(invalidBook, book.Id));
            Assert.Equal("Title", ex.ParamName);
        }

        [Fact]
        public void UpdateBook_WithEmptyAuthor_ShouldThrowArgumentException()
        {
            Book book = CreateBook();
            _bookService.AddBook(book);

            Book invalidBook = CreateBook(author: "");

			ArgumentException ex = Assert.Throws<ArgumentException>(() => _bookService.UpdateBook(invalidBook, book.Id));
            Assert.Equal("Author", ex.ParamName);
        }

        [Fact]
        public void UpdateBook_WithEmptyGenre_ShouldThrowArgumentException()
        {
            Book book = CreateBook();
            _bookService.AddBook(book);

            Book invalidBook = CreateBook(genre: "");

			ArgumentException ex = Assert.Throws<ArgumentException>(() => _bookService.UpdateBook(invalidBook, book.Id));
            Assert.Equal("Genre", ex.ParamName);
        }

        [Fact]
        public void UpdateBook_WithEmptyISBN_ShouldThrowArgumentException()
        {
            Book book = CreateBook();
            _bookService.AddBook(book);

            Book invalidBook = CreateBook(isbn: "");

			ArgumentException ex = Assert.Throws<ArgumentException>(() => _bookService.UpdateBook(invalidBook, book.Id));
            Assert.Equal("ISBN", ex.ParamName);
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

