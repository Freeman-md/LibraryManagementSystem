using System;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Tests.UnitTests.Services
{
	public partial class BookServiceTests
	{
		[Fact]
		public void GetAllBooks_ShouldReturnCollectionOfBooks()
		{
			for (int i = 1; i <= 10; i++)
			{
				_bookService.AddBook(CreateBook());
            }

			List<Book> books = _bookService.GetAllBooks();

			Assert.IsType<List<Book>>(books);
			Assert.NotEmpty(books);
        }

        [Fact]
		public void GetAllAvailableBooks_ShouldReturnCollectionOfBooks()
		{
			for (int i = 1; i <= 10; i++)
			{
				_bookService.AddBook(CreateBook(isAvailable: true));
            }

			List<Book> books = _bookService.GetAllAvailableBooks();

			Assert.IsType<List<Book>>(books);
			Assert.NotEmpty(books);
        }

        [Fact]
		public void GetAllAvailableBooks_WhenThereAreNone_ShouldReturnEmptyCollection()
		{
			for (int i = 1; i <= 10; i++)
			{
				_bookService.AddBook(CreateBook(isAvailable: false));
            }

			List<Book> books = _bookService.GetAllAvailableBooks();

			Assert.IsType<List<Book>>(books);
			Assert.Empty(books);
        }

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

