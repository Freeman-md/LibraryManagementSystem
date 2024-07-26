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
        private readonly JsonFileContext<Book> _jsonFileContext;
        private readonly string _testFilePath;

        public BookServiceFixture()
        {
            _testFilePath = Path.Combine(Path.GetTempPath(), $"book_service_test_{Guid.NewGuid()}.json");

            _jsonFileContext = new JsonFileContext<Book>();
            var bookRepository = new BookRepository(_jsonFileContext, _testFilePath);
            BookService = new BookService(bookRepository);
        }

        public void ClearData()
    {
        _jsonFileContext.WriteToFile(_testFilePath, new List<Book>());
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

