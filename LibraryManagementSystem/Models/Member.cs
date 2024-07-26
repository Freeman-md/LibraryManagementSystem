using System.Text.Json.Serialization;

namespace LibraryManagementSystem.Models
{
    public class Member
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime MembershipDate { get; private set; }

        [JsonConstructor]
        public Member(Guid id, string name, string email, string phoneNumber, string address)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            MembershipDate = DateTime.Now;
        }

        public Member(string name, string email, string phoneNumber, string address) : this(Guid.NewGuid(), name, email, phoneNumber, address) { }

        public Member(string name, string email) : this(Guid.NewGuid(), name, email, string.Empty, string.Empty) { }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var member = (Member)obj;
            return Id == member.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }


        public void PrintMemberDetails()
        {
            ConsoleHelper.PrintColoredText("Name: ", ConsoleColor.Green);
            ConsoleHelper.PrintColoredText($"{Name}, ", ConsoleColor.White);
            ConsoleHelper.PrintColoredText("Email: ", ConsoleColor.Green);
            ConsoleHelper.PrintColoredText($"{Email}, ", ConsoleColor.White);
            ConsoleHelper.PrintColoredText("Phone Number: ", ConsoleColor.Green);
            ConsoleHelper.PrintColoredText($"{PhoneNumber}, ", ConsoleColor.White);
            ConsoleHelper.PrintColoredText("Address: ", ConsoleColor.Green);
            ConsoleHelper.PrintColoredText($"{Address}", ConsoleColor.White);
            Console.WriteLine();
        }
    }
}

