using System;
using LibraryManagementSystem.FileContexts;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.Tests.TestHelpers
{
    public class BookRepositoryFixture : IDisposable
    {
        public BookRepository BookRepository { get; private set; }
        private readonly string _testFilePath;

        public BookRepositoryFixture()
        { 
            _testFilePath = Path.Combine(Path.GetTempPath(), $"book_repository_test_{Guid.NewGuid()}.json");

            JsonFileContext<Book> fileContext = new JsonFileContext<Book>();
            BookRepository = new BookRepository(fileContext, _testFilePath);
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

