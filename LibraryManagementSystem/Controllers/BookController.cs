namespace LibraryManagementSystem;

public class BookController
{
    public static void ShowMenu() {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nManage Books:");
        Console.WriteLine("1. Add Book");
        Console.WriteLine("2. Edit Book");
        Console.WriteLine("3. Delete Book");
        Console.WriteLine("4. View All Books");
        Console.WriteLine("5. Back to Main Menu");

        Console.Write("\nSelect an option: ");
        string? choice = Console.ReadLine();

        switch(choice) {
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

    private static void AddBook() {

    }

    private static void EditBook() {
        
    }

    private static void DeleteBook() {
        
    }

    private static void ViewAllBooks() {
        
    }
}
 