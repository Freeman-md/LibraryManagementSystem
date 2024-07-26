using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;
using LibraryManagementSystem.Tests.TestHelpers;

namespace LibraryManagementSystem.Tests;

public class SearchBookServiceTests : IClassFixture<SearchBookServiceFixture>
{
    // private readonly SearchBookServiceFixture _fixture;
    private readonly SearchBookService _searchBookService;
    private readonly BookService _bookService;

    public SearchBookServiceTests(SearchBookServiceFixture fixture)
    {   
        _searchBookService = fixture.SearchBookService;
        _bookService = fixture.BookService;

        fixture.ClearData();
    }

    private static Book CreateBook(
            string title = "Original Title",
            string author = "Original Author",
            string genre = "Original Genre",
            string isbn = "090-93080-3893",
            DateTime publishDate = default(DateTime),
            bool isAvailable = true) => new Book(title, author, genre, isbn, publishDate, isAvailable);

    [Fact]
    public void SearchBooks_WithMatchingTitle_ShouldReturnBooks()
    {
        List<Book> books = new List<Book> {
            CreateBook(title: "C# Programming", author: "Author A", genre: "Tech"),
            CreateBook(title: "Java Programming", author: "Author B", genre: "Tech"),
            CreateBook(title: "C# Advanced Programming", author: "Author C", genre: "Tech"),
        };

        const int expectedSearchResultsCount = 2;

        foreach (Book book in books)
        {
            _bookService.AddBook(book);
        }

        // Act
        List<Book> searchBookResults = _searchBookService.SearchBooks("C#");

        // Assert
        Assert.NotNull(books);
        Assert.Equal(expectedSearchResultsCount, searchBookResults.Count);
        Assert.Equal("C# Programming", searchBookResults.First().Title);
        Assert.Equal("C# Advanced Programming", searchBookResults[1].Title);
    }

    [Fact]
    public void SearchBooks_WithMatchingAuthor_ShouldReturnBooks()
    {
        List<Book> books = new List<Book> {
            CreateBook(title: "C# Programming", author: "Author A", genre: "Tech"),
            CreateBook(title: "Java Programming", author: "Author B", genre: "Tech"),
        };

        foreach (Book book in books)
        {
            _bookService.AddBook(book);
        }

        List<Book> searchBookResults = _searchBookService.SearchBooks("Author B");

        Assert.Single(searchBookResults);
        Assert.Equal("Java Programming", searchBookResults.First().Title);
    }

    [Fact]
    public void SearchBooks_WithMatchingGenre_ShouldReturnBooks()
    {
        List<Book> books = new List<Book> {
            CreateBook(title: "C# Programming", author: "Author A", genre: "Tech"),
            CreateBook(title: "Fiction Book", author: "Author B", genre: "Fiction"),
        };

        foreach (Book book in books)
        {
            _bookService.AddBook(book);
        }

        List<Book> searchBookResults = _searchBookService.SearchBooks("Tech");

        Assert.Single(searchBookResults);
        Assert.Equal("Tech", searchBookResults.First().Genre);
    }

    [Fact]
    public void SearchBooks_CaseInsensitive_ShouldReturnBooks()
    {
        List<Book> books = new List<Book> {
            CreateBook(title: "C# Programming", author: "Author A", genre: "Tech"),
            CreateBook(title: "Java Programming", author: "Author B", genre: "Tech"),
            CreateBook(title: "C# Advanced Programming", author: "Author C", genre: "Tech"),
        };

        foreach (Book book in books)
        {
            _bookService.AddBook(book);
        }

        List<Book> searchBookResults = _searchBookService.SearchBooks("c# programming");

        Assert.Single(searchBookResults);
        Assert.Equal("C# Programming", searchBookResults.First().Title);
    }

    [Fact]
    public void SearchBooks_NoMatches_ShouldReturnEmptyList()
    {
        _bookService.AddBook(CreateBook(title: "C# Programming", author: "Author A", genre: "Tech"));

        List<Book> searchBookResults = _searchBookService.SearchBooks("non-existing");
        
        Assert.Empty(searchBookResults);
    }

    // [Fact]
    // public void SearchBooks_NoSearchTerm_ShouldThrowArgumentNullException()
    // {
    //     Assert.Throws<ArgumentNullException>(() => _searchBookService.SearchBooks(null!));
    // }
}
