using LibraryManagementSystem.FileContexts;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem;

public class BookController
{
    private readonly static BookService _bookService;

    static BookController()
    {
        _bookService = new BookService(new BookRepository(new JsonFileContext<Book>()));
    }

    public static void ShowMenu()
    {
        DisplayTotalBooksCount();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nManage Books:");
        Console.WriteLine("1. Add Book");
        Console.WriteLine("2. Edit Book");
        Console.WriteLine("3. Delete Book");
        Console.WriteLine("4. View All Books");
        Console.WriteLine("5. Back to Main Menu");

        Console.Write("\nSelect an option: ");
        string? choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                AddBook();
                break;
            case "2":
                EditBook();
                break;
            case "3":
                DeleteBook();
                break;
            case "4":
                ViewAllBooks();
                break;
            case "5":
                Program.ShowMainMenu();
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid option. Please try again.");
                Console.ResetColor();

                ShowMenu();
                break;
        }
    }

    private static void AddBook()
    {
        try
        {
            string title = InputValidator.GetValidInput("Enter Book Title: ", "Title cannot be empty.");
            string author = InputValidator.GetValidInput("Enter Book Author: ", "Author cannot be empty.");
            string genre = InputValidator.GetValidInput("Enter Book Genre: ", "Genre cannot be empty.");
            string isbn = InputValidator.GetValidInput("Enter Book ISBN: ", "ISBN cannot be empty.");

            var book = new Book(title, author, genre, isbn);

            _bookService.AddBook(book);

            Console.WriteLine("Book added successfully.");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("An error occurred while adding the book: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }
        finally
        {
            ShowMenu();
        }
    }

    private static void EditBook()
    {

    }

    private static void DeleteBook()
    {

    }

    private static void ViewAllBooks()
    {

    }

    private static void DisplayTotalBooksCount()
    {
        try
        {
            int totalBooks = _bookService.GetAllBooks().Count;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nTotal Books in Library: {totalBooks}");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("An error occurred while retrieving the total number of books: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }
    }

}
