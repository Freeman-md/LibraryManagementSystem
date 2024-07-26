namespace LibraryManagementSystem;

public class SearchController
{
    public static void ShowMenu() {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nSearch Books:");
        Console.WriteLine("1. Search by Title");
        Console.WriteLine("2. Search by Author");
        Console.WriteLine("3. Search by Genre");
        Console.WriteLine("4. Back to Main Menu");

        Console.Write("\nSelect an option: ");
        string? choice = Console.ReadLine();

        switch(choice) {
            case "1": 
                SearchByTitle();
                break;
            case "2":
                SearchByAuthor();
                break;
            case "3": 
                SearchByGenre();
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

    private static void SearchByTitle() {

    }

    private static void SearchByAuthor() {
        
    }

    private static void SearchByGenre() {
        
    }
}
