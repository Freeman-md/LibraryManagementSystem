using System;
namespace LibraryManagementSystem.Models
{
	public class Transaction
	{
		public int Id { get; private set; }
        public Book Book { get; private set; }
		public Member Member { get; private set; }
		public DateTime TransactionDate { get; set; }

        public Transaction(int id, Book book, Member member, DateTime transactionDate)
		{
			Id = id;
			Book = book;
			Member = member;
			TransactionDate = transactionDate;
		}
	}
}

