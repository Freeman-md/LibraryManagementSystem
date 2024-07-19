using LibraryManagementSystem.FileContexts;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Tests;

public class SearchBookServiceFixture
{
    public SearchBookService SearchBookService;
    public BookService BookService;
    private readonly string _testFilePath;

    public SearchBookServiceFixture()
    {
        _testFilePath = Path.Combine(Path.GetTempPath(), $"search_book_service_tests_{Guid.NewGuid()}.json");

        JsonFileContext<Book> jsonFileContext = new JsonFileContext<Book>();
        BookRepository bookRepository = new BookRepository(jsonFileContext);

        SearchBookService = new SearchBookService(bookRepository);
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
