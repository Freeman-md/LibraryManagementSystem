using System;
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
		public void AddBook_ShouldAddBookSuccessfully()
		{
            // TODO: Arrange

            // TODO: Act

            // TODO: Assert
        }
    }
}

