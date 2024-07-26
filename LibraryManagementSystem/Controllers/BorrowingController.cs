using LibraryManagementSystem.FileContexts;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem;

public class BorrowingController
{
    private readonly static BorrowingTransactionService _borrowingTransactionService;
    private readonly static BookService _bookService;
    private readonly static MemberService _memberService;

    static BorrowingController()
    {
        var bookRepository = new BookRepository(new JsonFileContext<Book>());
        var memberRepository = new MemberRepository(new JsonFileContext<Member>());
        var borrowingTransactionRepository = new BorrowingTransactionRepository(new JsonFileContext<BorrowingTransaction>());

        _bookService = new BookService(bookRepository);
        _memberService = new MemberService(memberRepository);
        _borrowingTransactionService = new BorrowingTransactionService(borrowingTransactionRepository, _bookService, _memberService);
    }

    public static void ShowMenu()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nBorrowing and Returning:");
        Console.WriteLine("1. Borrow Book");
        Console.WriteLine("2. Return Book");
        Console.WriteLine("3. View All Borrowed Books");
        Console.WriteLine("4. Back to Main Menu");

        Console.Write("\nSelect an option: ");
        string? choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                BorrowBook();
                break;
            case "2":
                ReturnBook();
                break;
            case "3":
                ViewAllBorrowedBooks();
                break;
            case "4":
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

    private static void BorrowBook()
    {
        try
        {
            var availableBooks = _bookService.GetAllAvailableBooks();

            if (availableBooks.Count == 0)
            {
                Console.WriteLine("No available books to borrow.");
                ShowMenu();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nAvailable Books:");
            Console.ResetColor();

            for (int i = 0; i < availableBooks.Count; i++)
            {
                var book = availableBooks[i];
                Console.Write($"{i + 1}. ");
                book.PrintBookDetails();
            }

            Console.WriteLine("\nEnter the number of the book you want to borrow:");
            if (!int.TryParse(Console.ReadLine(), out int bookNumber) || bookNumber < 1 || bookNumber > availableBooks.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid book number.");
                Console.ResetColor();
                ShowMenu();
                return;
            }

            var selectedBook = availableBooks[bookNumber - 1];

            var members = _memberService.GetAllMembers();

            if (members.Count == 0)
            {
                Console.WriteLine("No registered members.");
                ShowMenu();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nRegistered Members:");
            Console.ResetColor();

            for (int i = 0; i < members.Count; i++)
            {
                var member = members[i];
                Console.Write($"{i + 1}. ");
                member.PrintMemberDetails();
            }

            Console.WriteLine("\nEnter the number of the member borrowing the book:");
            if (!int.TryParse(Console.ReadLine(), out int memberNumber) || memberNumber < 1 || memberNumber > members.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid member number.");
                Console.ResetColor();
                ShowMenu();
                return;
            }

            var selectedMember = members[memberNumber - 1];

            Console.WriteLine($"Enter the duration in days for borrowing the book (default: {BorrowingTransaction.DEFAULT_BORROWING_DURATION_IN_DAYS} days):");
            string? input = Console.ReadLine();
            int duration = string.IsNullOrWhiteSpace(input) ? BorrowingTransaction.DEFAULT_BORROWING_DURATION_IN_DAYS : int.Parse(input);

            _borrowingTransactionService.BorrowBook(selectedBook.Id, selectedMember.Id);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Book borrowed successfully.");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("An error occurred while borrowing the book: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }
        finally
        {
            ShowMenu();
        }
    }

    private static void ReturnBook()
    {
        try
        {
            var borrowedBooks = _borrowingTransactionService.GetAllBorrowedBooks();

            if (borrowedBooks.Count == 0)
            {
                Console.WriteLine("No borrowed books to return.");
                ShowMenu();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nBorrowed Books:");
            Console.ResetColor();

            for (int i = 0; i < borrowedBooks.Count; i++)
            {
                var transaction = borrowedBooks[i];
                Console.Write($"{i + 1}. ");
                transaction.PrintDetails();
            }

            Console.WriteLine("\nEnter the number of the book you want to return:");
            if (!int.TryParse(Console.ReadLine(), out int transactionNumber) || transactionNumber < 1 || transactionNumber > borrowedBooks.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid number.");
                Console.ResetColor();
                ShowMenu();
                return;
            }

            var selectedTransaction = borrowedBooks[transactionNumber - 1];

            _borrowingTransactionService.ReturnBook(selectedTransaction.Book.Id, selectedTransaction.Member.Id);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Book returned successfully.");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("An error occurred while returning the book: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }
        finally
        {
            ShowMenu();
        }
    }

    private static void ViewAllBorrowedBooks()
    {
        try
        {
            var borrowedBooks = _borrowingTransactionService.GetAllBorrowedBooks();

            if (borrowedBooks.Count == 0)
            {
                Console.WriteLine("No borrowed books.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nBorrowed Books:");
                Console.ResetColor();

                foreach (var transaction in borrowedBooks)
                {
                    transaction.PrintDetails();
                }
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("An error occurred while retrieving the list of borrowed books: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }
        finally
        {
            ShowMenu();
        }
    }
}
