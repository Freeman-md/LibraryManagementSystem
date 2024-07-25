using LibraryManagementSystem.FileContexts;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Tests;

public class SearchBookServiceFixture : IDisposable
{
    public SearchBookService SearchBookService;
    public BookService BookService;
    private readonly string _testFilePath;
    private readonly JsonFileContext<Book> _jsonFileContext;

    public SearchBookServiceFixture()
    {
        _testFilePath = Path.Combine(Path.GetTempPath(), $"search_book_service_tests_{Guid.NewGuid()}.json");

        _jsonFileContext = new JsonFileContext<Book>();
        BookRepository bookRepository = new BookRepository(_jsonFileContext, _testFilePath);

        SearchBookService = new SearchBookService(bookRepository);
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

