using LibraryManagementSystem;

public class Program {
    static void Main(string[] args) {
        Console.Title = "Library Management System";
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("*************************************");
        Console.WriteLine("* Welcome to the Library Management *");
        Console.WriteLine("*            System                 *");
        Console.WriteLine("*************************************");
        Console.ResetColor();

        ShowMainMenu();
    }

    public static void ShowMainMenu()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nMain Menu:");
        Console.ResetColor();
        Console.WriteLine("1. Manage Books");
        Console.WriteLine("2. Manage Members");
        Console.WriteLine("3. Borrowing and Returning");
        Console.WriteLine("4. Search Books");
        Console.WriteLine("5. Exit");

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("\nSelect an option (1-5): ");
        Console.ResetColor();
        string? choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                BookController.ShowMenu();
                break;
            case "2":
                MemberController.ShowMenu();
                break;
            case "3":
                BorrowingController.ShowMenu();
                break;
            case "4":
                SearchController.ShowMenu();
                break;
            case "5":
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Exiting... Thank you for using the Library Management System!");
                Console.ResetColor();
                Environment.Exit(0);
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid option. Please try again.");
                Console.ResetColor();
                ShowMainMenu();
                break;
        }
    }
}