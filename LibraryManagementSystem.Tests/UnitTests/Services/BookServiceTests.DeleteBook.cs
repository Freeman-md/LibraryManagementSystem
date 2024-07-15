using System;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Tests.UnitTests.Services
{
	public partial class BookServiceTests
	{
		[Fact]
		public void DeleteBook_ShouldRemoveBookSuccessfully()
		{
            Book bookToDelete = CreateBook();
			_bookService.AddBook(bookToDelete);

			_bookService.DeleteBook(bookToDelete.Id);

            Book? nullBook = _bookService.GetBookById(bookToDelete.Id);

			Assert.Null(nullBook);
		}

		[Fact]
		public void DeleteBook_WhenItemDoesNotExist_ShouldThrowArgumentException()
		{
            Book notSavedBook = CreateBook();

			Assert.Throws<ArgumentException>(() => _bookService.DeleteBook(notSavedBook.Id));
        }
	}
}

