using System;
using System.Text.Json.Serialization;
namespace LibraryManagementSystem.Models
{
	public class Transaction
	{
		public Guid Id { get; private set; }
        public Book Book { get; private set; }
		public Member Member { get; private set; }
		protected DateTime TransactionDate { get; private set; }

		[JsonConstructor]
        public Transaction(Guid id, Book book, Member member, DateTime transactionDate = default(DateTime))
		{
			Id = id;
			Book = book;
			Member = member;
			TransactionDate = transactionDate;
		}

		public Transaction(Book book, Member member) : this(Guid.NewGuid(), book, member) {}
	}
}

