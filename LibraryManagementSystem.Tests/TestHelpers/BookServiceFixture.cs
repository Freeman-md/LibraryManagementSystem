using System;
using LibraryManagementSystem.FileContexts;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Tests.TestHelpers
{
	public class BookServiceFixture : IDisposable
	{
        public BookService BookService { get; private set; }

        public BookServiceFixture()
        {
            JsonFileContext<Book> fileContext = new JsonFileContext<Book>();
            var bookRepository = new BookRepository(fileContext);
            BookService = new BookService(bookRepository);
        }

        public void Dispose()
        {
            // Clean up if necessary
        }
    }
}

