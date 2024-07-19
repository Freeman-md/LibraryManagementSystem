using LibraryManagementSystem.Models;
using LibraryManagementSystem.Tests.TestHelpers;

namespace LibraryManagementSystem.Tests;

public class SearchBookServiceTests : IClassFixture<SearchBookServiceFixture>
{
    private readonly SearchBookService _searchBookService;

    private static Book CreateBook(
            string title = "Original Title",
            string author = "Original Author",
            string genre = "Original Genre",
            string isbn = "090-93080-3893",
            DateTime publishDate = default(DateTime),
            bool isAvailable = true) => new Book(title, author, genre, isbn, publishDate, isAvailable);

    public SearchBookServiceTests(SearchBookServiceFixture fixture) {
        _searchBookService = fixture.SearchBookService;
    }

    [Fact]
    public void SearchBooks_WithMatchingTitle_ShouldReturnBooks() {
        // Arrange

        // Act

        // Assert
    }

    [Fact]
    public void SearchBooks_WithMatchingAuthor_ShouldReturnBooks() {
        // Arrange

        // Act

        // Assert
    }

    [Fact]
    public void SearchBooks_WithMatchingGenre_ShouldReturnBooks() {
        // Arrange

        // Act

        // Assert
    }

    [Fact]
    public void SearchBooks_CaseInsensitive_ShouldReturnBooks() {
        // Arrange

        // Act

        // Assert
    }

    [Fact]
    public void SearchBooks_NoMatches_ShouldReturnEmptyList() {
        // Arrange

        // Act

        // Assert
    }
}
