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
        List<Book> books = new List<Book>();

        try
        {
            books = RetrieveAndDisplayBooks();

            if (books.Count == 0)
            {
                ShowMenu();
                return;
            }

            Console.WriteLine("\nEnter the number of the book you want to edit:");
            if (!int.TryParse(Console.ReadLine(), out int bookNumber) || bookNumber < 1 || bookNumber > _bookService.GetAllBooks().Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid book number.");
                Console.ResetColor();
                ShowMenu();
                return;
            }

            var bookToEdit = books[bookNumber - 1];

            string title = InputValidator.GetValidInput($"Enter new Book Title (current: {bookToEdit.Title}) or press Enter to keep current: ", "Title cannot be empty.", bookToEdit.Title);
            string author = InputValidator.GetValidInput($"Enter new Book Author (current: {bookToEdit.Author}) or press Enter to keep current: ", "Author cannot be empty.", bookToEdit.Author);
            string genre = InputValidator.GetValidInput($"Enter new Book Genre (current: {bookToEdit.Genre}) or press Enter to keep current: ", "Genre cannot be empty.", bookToEdit.Genre);
            string isbn = InputValidator.GetValidInput($"Enter new Book ISBN (current: {bookToEdit.ISBN}) or press Enter to keep current: ", "ISBN cannot be empty.", bookToEdit.ISBN);

            var updatedBook = new Book(bookToEdit.Id, title, author, genre, isbn, bookToEdit.PublishDate, bookToEdit.IsAvailable);
            _bookService.UpdateBook(updatedBook, bookToEdit.Id);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Book edited successfully.");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("An error occurred while editing the book: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }
        finally
        {
            ShowMenu();
        }
    }


    private static void DeleteBook()
    {
        List<Book> books = new List<Book>();

        try
        {
            books = RetrieveAndDisplayBooks();

            if (books.Count == 0)
            {
                ShowMenu();
                return;
            }

            Console.WriteLine("\nEnter the number of the book you want to delete:");
            if (!int.TryParse(Console.ReadLine(), out int bookNumber) || bookNumber < 1 || bookNumber > books.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid book number.");
                Console.ResetColor();
                ShowMenu();
                return;
            }

            var bookToDelete = books[bookNumber - 1];

            Console.WriteLine($"Are you sure you want to delete the book: {bookToDelete.Title}? (yes/no)");
            string? confirmation = Console.ReadLine()?.ToLower();

            if (confirmation == "yes")
            {
                _bookService.DeleteBook(bookToDelete.Id);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Book deleted successfully.");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("Book deletion cancelled.");
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("An error occurred while deleting the book: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }
        finally
        {
            ShowMenu();
        }
    }


    private static void ViewAllBooks()
    {
        try
        {
            RetrieveAndDisplayBooks();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("An error occurred while retrieving the list of books: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }
        finally
        {
            ShowMenu();
        }
    }


    private static List<Book> RetrieveAndDisplayBooks()
    {
        var books = _bookService.GetAllBooks();

        if (books.Count == 0)
        {
            Console.WriteLine("No books available.");
            return books;
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\nList of Books:");
        Console.ResetColor();

        for (int i = 0; i < books.Count; i++)
        {
            var book = books[i];
            Console.Write($"{i + 1}. ");
            book.PrintBookDetails();
        }

        return books;
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
