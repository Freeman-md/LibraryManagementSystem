using LibraryManagementSystem.FileContexts;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem;

public class SearchController
{
    private readonly static SearchBookService _searchBookService;

    static SearchController()
    {
        _searchBookService = new SearchBookService(new BookRepository(new JsonFileContext<Book>()));
    }

    public static void ShowMenu()
    {
        BookController.DisplayTotalBooksCount();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nSearch Books");
        Console.ResetColor();
        PerformSearch();
    }

    private static void PerformSearch()
    {
        Console.Write("Enter the book title, author, or genre to search for: ");
        string searchTerm = Console.ReadLine() ?? string.Empty;

        try
        {
            var searchResults = _searchBookService.SearchBooks(searchTerm);
            DisplaySearchResults(searchResults);
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("An error occurred while searching for books: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }
        finally
        {
            ShowMenu();
        }
    }

    private static void DisplaySearchResults(List<Book> searchResults)
    {
        if (searchResults.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No books found matching the search criteria.");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nSearch Results:");
            Console.ResetColor();

            for (int i = 0; i < searchResults.Count; i++)
            {
                var book = searchResults[i];
                Console.Write($"{i + 1}. ");
                book.PrintBookDetails();
            }

            Console.WriteLine("\nSelect a book number to perform operations on it or press Enter to return to the main menu:");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Program.ShowMainMenu();
                return;
            }
            
            if (int.TryParse(input, out int bookNumber) && bookNumber >= 1 && bookNumber <= searchResults.Count)
            {
                var selectedBook = searchResults[bookNumber - 1];
                BookController.ManageSelectedBook(selectedBook);
            }
        }
    }
}
