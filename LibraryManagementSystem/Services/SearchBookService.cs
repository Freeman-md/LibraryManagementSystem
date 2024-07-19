using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem;

public class SearchBookService
{
    private BookRepository _bookRepository;
    public SearchBookService(BookRepository bookRepository) {
        _bookRepository = bookRepository;
    }

    public List<Book> SearchBooks(string searchTerm) {
        throw new NotImplementedException();
    }
}
