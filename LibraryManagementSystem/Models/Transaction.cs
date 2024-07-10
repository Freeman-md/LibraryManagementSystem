using System;
namespace LibraryManagementSystem.Models
{
	public class Transaction
	{
		public int Id { get; private set; }
        public int BookId { get; private set; }
		public int MemberId { get; private set; }
		public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ReturnDate { get; set; }
		public double Fine { get; set; }

        public Transaction(int id, int bookId, int memberId, DateTime borrowDate, DateTime dueDate, DateTime returnDate, double fine)
		{
			Id = id;
			BookId = bookId;
			MemberId = memberId;
			BorrowDate = borrowDate;
			DueDate = dueDate;
			ReturnDate = returnDate;
			Fine = fine;
		}
	}
}

