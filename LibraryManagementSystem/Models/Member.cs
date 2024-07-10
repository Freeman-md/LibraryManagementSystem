using System;
namespace LibraryManagementSystem.Models
{
	public class Member
	{
        public int Id { get; private set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime MembershipDate { get; private set; }
        public string Address { get; set; }

        public List<Transaction> BorrowedBooks
        {
            get
            {
                // TODO: get member borrowed books by MemberID on transactions
                return new List<Transaction>();
            }
        }

        public Member(int id, string email, string phoneNumber, DateTime membershipDate, string address)
		{
            Id = id;
            Email = email;
            PhoneNumber = phoneNumber;
            MembershipDate = membershipDate;
            Address = address;
		}
	}
}

