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
        private readonly string _testFilePath = "books_test.json";

        public BookServiceFixture()
        {
            JsonFileContext<Book> fileContext = new JsonFileContext<Book>();
            var bookRepository = new BookRepository(fileContext, _testFilePath);
            BookService = new BookService(bookRepository);
        }

        public void Dispose()
        {
            if (System.IO.File.Exists(_testFilePath))
            {
                System.IO.File.Delete(_testFilePath);
            }
        }
    }
}

