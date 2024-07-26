namespace LibraryManagementSystem;

public class BorrowingController
{
    public static void ShowMenu() {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nBorrowing and Returning:");
        Console.WriteLine("1. Borrow Book");
        Console.WriteLine("2. Return Book");
        Console.WriteLine("3. View All Borrowed Books");
        Console.WriteLine("4. Back to Main Menu");

        Console.Write("\nSelect an option: ");
        string? choice = Console.ReadLine();

        switch(choice) {
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

    private static void BorrowBook() {

    }

    private static void ReturnBook() {
        
    }

    private static void ViewAllBorrowedBooks() {
        
    }
}
