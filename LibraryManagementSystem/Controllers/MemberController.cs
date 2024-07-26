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
        List<Member> members = new List<Member>();

        try
        {
            members = RetrieveAndDisplayMembers();

            if (members.Count == 0)
            {
                ShowMenu();
                return;
            }

            Console.WriteLine("\nEnter the number of the member you want to edit:");
            if (!int.TryParse(Console.ReadLine(), out int memberNumber) || memberNumber < 1 || memberNumber > members.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid member number.");
                Console.ResetColor();
                ShowMenu();
                return;
            }

            var memberToEdit = members[memberNumber - 1];

            string name = InputValidator.GetValidInput($"Enter new Member Name (current: {memberToEdit.Name}) or press Enter to keep current: ", "Name cannot be empty.", memberToEdit.Name);
            string email = InputValidator.GetValidInput($"Enter new Member Email (current: {memberToEdit.Email}) or press Enter to keep current: ", "Email cannot be empty.", memberToEdit.Email);
            string phoneNumber = InputValidator.GetValidInput($"Enter new Member Phone Number (current: {memberToEdit.PhoneNumber}) or press Enter to keep current: ", "Phone number cannot be empty.", memberToEdit.PhoneNumber);
            string address = InputValidator.GetValidInput($"Enter new Member Address (current: {memberToEdit.Address}) or press Enter to keep current: ", "Address cannot be empty.", memberToEdit.Address);

            var updatedMember = new Member(memberToEdit.Id, name, email, phoneNumber, address);
            _memberService.UpdateMember(updatedMember, memberToEdit.Id);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Member edited successfully.");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("An error occurred while editing the member: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }
        finally
        {
            ShowMenu();
        }
    }


    private static void DeleteMember()
    {
        List<Member> members = new List<Member>();

        try
        {
            members = RetrieveAndDisplayMembers();

            if (members.Count == 0)
            {
                ShowMenu();
                return;
            }

            Console.WriteLine("\nEnter the number of the member you want to delete:");
            if (!int.TryParse(Console.ReadLine(), out int memberNumber) || memberNumber < 1 || memberNumber > members.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid member number.");
                Console.ResetColor();
                ShowMenu();
                return;
            }

            var memberToDelete = members[memberNumber - 1];

            Console.WriteLine($"Are you sure you want to delete the member: {memberToDelete.Name}? (yes/no)");
            string? confirmation = Console.ReadLine()?.ToLower();

            if (confirmation == "yes")
            {
                _memberService.DeleteMember(memberToDelete.Id);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Member deleted successfully.");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("Member deletion cancelled.");
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("An error occurred while deleting the member: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }
        finally
        {
            ShowMenu();
        }
    }


    private static void ViewAllMembers()
    {
        List<Member> members = new List<Member>();

        try
        {
            members = RetrieveAndDisplayMembers();

            if (members.Count == 0)
            {
                ShowMenu();
                return;
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("An error occurred while retrieving the list of members: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }
        finally
        {
            ShowMenu();
        }
    }

    private static List<Member> RetrieveAndDisplayMembers()
    {
        var members = _memberService.GetAllMembers();

        if (members.Count == 0)
        {
            Console.WriteLine("No members available.");
            return members;
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\nList of Members:");
        Console.ResetColor();

        for (int i = 0; i < members.Count; i++)
        {
            var member = members[i];
            Console.Write($"{i + 1}. ");
            member.PrintMemberDetails();
        }

        return members;
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
