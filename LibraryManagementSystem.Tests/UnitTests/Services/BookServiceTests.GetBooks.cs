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
	}
}

