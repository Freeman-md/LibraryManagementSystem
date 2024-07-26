namespace LibraryManagementSystem;

public class MemberController
{
    public static void ShowMenu() {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nManage Members:");
        Console.WriteLine("1. Add Member");
        Console.WriteLine("2. Edit Member");
        Console.WriteLine("3. Delete Member");
        Console.WriteLine("4. View All Members");
        Console.WriteLine("5. Back to Main Menu");

        Console.Write("\nSelect an option: ");
        string? choice = Console.ReadLine();

        switch(choice) {
            case "1": 
                AddMember();
                break;
            case "2":
                EditMember();
                break;
            case "3": 
                DeleteMember();
                break;
            case "4":
                ViewAllMembers();
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

    private static void AddMember() {

    }

    private static void EditMember() {
        
    }

    private static void DeleteMember() {
        
    }

    private static void ViewAllMembers() {
        
    }
}
