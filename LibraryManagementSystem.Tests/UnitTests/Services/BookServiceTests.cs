using System;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Tests.TestHelpers;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Tests.UnitTests.Services
{
	public partial class BookServiceTests
	{
        private readonly BookService _bookService;

        public BookServiceTests(BookServiceFixture fixture)
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
    }
}

