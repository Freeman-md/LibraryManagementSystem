using System;
using System.Text.Json.Serialization;
namespace LibraryManagementSystem.Models
{
	public class Transaction
	{
		public Guid Id { get; private set; }
        public Book Book { get; private set; }
		public Member Member { get; private set; }
		public DateTime TransactionDate { get; private set; }

		[JsonConstructor]
        public Transaction(Guid id, Book book, Member member, DateTime transactionDate = default(DateTime))
		{
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            Book = book;
			Member = member;
            TransactionDate = transactionDate == default(DateTime) ? DateTime.Now : transactionDate;
		}

		public Transaction(Book book, Member member) : this(Guid.NewGuid(), book, member) {}

		public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var transaction = (Transaction)obj;
            return Id == transaction.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
	}
}

