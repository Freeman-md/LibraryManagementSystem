using System;
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

        public List<Transaction> BorrowedBooks
        {
            get
            {
                // TODO: get member borrowed books by MemberID on transactions
                return new List<Transaction>();
            }
        }

        [JsonConstructor]
        public Member(Guid id, string name, string email, string phoneNumber, string address, DateTime membershipDate = default(DateTime))
		{
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            MembershipDate = membershipDate == default(DateTime) ? DateTime.Now : membershipDate;
        }

        public Member(string name, string email, string phoneNumber, string address, DateTime membershipDate = default(DateTime)) : this(Guid.NewGuid(), name, email, phoneNumber, address, DateTime.Now) { }

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
    }
}

