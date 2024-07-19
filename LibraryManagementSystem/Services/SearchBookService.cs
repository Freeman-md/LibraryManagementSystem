using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem;

public class SearchBookService
{
    private BookRepository _bookRepository;
    public SearchBookService(BookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public List<Book> SearchBooks(string searchTerm)
    {
        ValidateSearchTerm(searchTerm);

        List<Book> books = _bookRepository.GetAllBooks();

        return books.Where(book =>
            book.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            book.Author.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            book.Genre.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
        ).ToList();
    }

    private void ValidateSearchTerm(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            throw new ArgumentNullException("Search term is required.", nameof(searchTerm));
        }
    }
}
