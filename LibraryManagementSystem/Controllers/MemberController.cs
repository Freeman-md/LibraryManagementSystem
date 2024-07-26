using LibraryManagementSystem.FileContexts;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem;

public class MemberController
{
    private readonly static MemberService _memberService;

    static MemberController()
    {
        _memberService = new MemberService(new MemberRepository(new JsonFileContext<Member>()));
    }

    public static void ShowMenu()
    {
        DisplayTotalMembersCount();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nManage Members:");
        Console.WriteLine("1. Add Member");
        Console.WriteLine("2. Edit Member");
        Console.WriteLine("3. Delete Member");
        Console.WriteLine("4. View All Members");
        Console.WriteLine("5. Back to Main Menu");

        Console.Write("\nSelect an option: ");
        string? choice = Console.ReadLine();

        switch (choice)
        {
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

    private static void AddMember()
    {
        try
        {
            string name = InputValidator.GetValidInput("Enter Member Name: ", "Name cannot be empty.");
            string email = InputValidator.GetValidInput("Enter Member Email: ", "Email cannot be empty.");
            string phoneNumber = InputValidator.GetValidInput("Enter Member Phone Number: ", "Phone number cannot be empty.");
            string address = InputValidator.GetValidInput("Enter Member Address: ", "Address cannot be empty.");

            var member = new Member(name, email, phoneNumber, address);

            _memberService.RegisterMember(member);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Member added successfully.");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("An error occurred while adding the member: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }
        finally
        {
            ShowMenu();
        }
    }

    private static void EditMember()
    {

    }

    private static void DeleteMember()
    {

    }

    private static void ViewAllMembers()
    {

    }

    private static void DisplayTotalMembersCount()
    {
        try
        {
            int totalMembers = _memberService.GetAllMembers().Count;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nTotal Members Registered: {totalMembers}");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("An error occurred while retrieving the total number of members: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }
    }
}
